using System.Globalization;
using System.Net;
using System.Security;
using WorldClimateSolution.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

WebClient webClient = new();
webClient.DownloadFile(
    "https://raw.githubusercontent.com/JorenVdk777/WorldClimateSolution/rlaevaert/BackEndAdded/WorldClimateSolutionAPI/WorldClimateSolution.gRPC/cities_air_quality_water_pollution.18-10-2021.csv",
    "airpolWaterPol");
var airpolWaterPol = File.ReadAllLines("airpolWaterPol").Skip(1)
    .Select(x => x.Split(',').ToList())
    .Select(x => new CityStats()
    {
        City = x[0].Replace("\"", "").Trim(),
        Country = x[2].Replace("\"", "").Trim(),
        Region = x[1].Replace("\"", "").Trim(),
        WaterQuality = Convert.ToDouble(x[3].Trim().Replace('.', ',')),
        AirQuality = Convert.ToDouble(x[4].Trim().Replace('.', ','))
    }).Where(x => x.AirQuality is not (0 or 100) && x.WaterQuality is not (0 or 100)).ToList();

webClient.DownloadFile(
    "https://raw.githubusercontent.com/JorenVdk777/WorldClimateSolution/rlaevaert/BackEndAdded/WorldClimateSolutionAPI/WorldClimateSolution.gRPC/greenAreaPerCapita_DezeGerbuikenRobin.csv",
    "green");
var green = File.ReadAllLines("green").Skip(1)
    .Select(x => x.Split(",").ToList())
    .Select(x => new GreenPerCapita
    {
        Place = x[0],
        Year = Convert.ToInt32(x[1]),
        Green = Convert.ToDouble(x[2].Replace(".", ","))
    })
    .GroupBy(x => x.Place)
    .ToList();

Memory.Overview = new CityStatsOverview()
{
    CityStats = airpolWaterPol,
    AverageAirQuality = airpolWaterPol.Average(x => x.AirQuality),
    AverageWaterPollution = airpolWaterPol.Average(x => x.WaterQuality),
    MinAirQuality = airpolWaterPol.Min(x => x.AirQuality),
    MinWaterQuality = airpolWaterPol.Min(x => x.WaterQuality),
    MaxAirQuality = airpolWaterPol.Max(x => x.AirQuality),
    MaxWaterQuality = airpolWaterPol.Max(x => x.WaterQuality),
    MedianAirQuality = airpolWaterPol.OrderBy(x => x.AirQuality)
        .Skip((int) Math.Floor((double) airpolWaterPol.Count / 2)).First().AirQuality,
    MedianWaterQuality = airpolWaterPol.OrderBy(x => x.WaterQuality)
        .Skip((int) Math.Floor((double) airpolWaterPol.Count / 2)).First().WaterQuality,
};

Memory.GreenPerCapita = green.Select(x => new GreenPerCapitaOverview()
{
    Place = x.Key,
    Green = x.Select(y => Tuple.Create(y.Year, y.Green)).ToList()
}).ToList();

// Configure the HTTP request pipeline.
app.MapGet("/City-Stats/Overview", () => Memory.Overview);
app.MapGet("/City-Stats/Green", () => Memory.GreenPerCapita);

app.Run();


string GetCSV(string url)
{
    HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
    HttpWebResponse resp = (HttpWebResponse) req.GetResponse();

    StreamReader sr = new StreamReader(resp.GetResponseStream());
    string results = sr.ReadToEnd();
    sr.Close();

    return results;
}

public class GreenPerCapitaOverview
{
    public string Place { get; set; }
    public List<Tuple<int, double>> Green { get; set; }
}

public class GreenPerCapita
{
    public string Place { get; set; }
    public int Year { get; set; }
    public double Green { get; set; }
}

public class CityStatsOverview
{
    public List<CityStats> CityStats { get; set; }
    public double AverageAirQuality { get; set; }
    public double AverageWaterPollution { get; set; }
    public double MaxAirQuality { get; set; }
    public double MinAirQuality { get; set; }
    public double MaxWaterQuality { get; set; }
    public double MinWaterQuality { get; set; }
    public double MedianWaterQuality { get; set; }
    public double MedianAirQuality { get; set; }
}

public class CityStats
{
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public double AirQuality { get; set; }
    public double WaterQuality { get; set; }
}

public static class Memory
{
    public static CityStatsOverview Overview { get; set; }
    public static List<GreenPerCapitaOverview> GreenPerCapita { get; set; }
}