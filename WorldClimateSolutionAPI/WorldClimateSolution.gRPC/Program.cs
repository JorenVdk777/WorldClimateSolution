using System.Net;
using WorldClimateSolution.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();
//var test = GetCSV("https://raw.githubusercontent.com/JorenVdk777/WorldClimateSolution/rlaevaert/BackEndAdded/WorldClimateSolutionAPI/WorldClimateSolution.gRPC/cities_air_quality_water_pollution.18-10-2021.csv").Split(',').Skip(1).Select(x => x).ToList();
//var airpolWaterPol = test[456];


WebClient webClient = new();
var temp5 = Convert.ToDouble("4.5".Replace('.',','));
webClient.DownloadFile("https://raw.githubusercontent.com/JorenVdk777/WorldClimateSolution/rlaevaert/BackEndAdded/WorldClimateSolutionAPI/WorldClimateSolution.gRPC/cities_air_quality_water_pollution.18-10-2021.csv", "airpolWaterPol");
var airpolWaterPol = File.ReadAllLines("airpolWaterPol").Skip(1).Select(x => x.Split(',').ToList()).Select(x => new CityStats(){City = x[0], Country = x[2], Region=x[1], WaterQuality = Convert.ToDouble(x[4].Trim().Replace('.',',')), AirQuality = Convert.ToDouble(x[5].Trim().Replace('.',','))}).ToList();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();


string GetCSV(string url)
{
    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

    StreamReader sr = new StreamReader(resp.GetResponseStream());
    string results = sr.ReadToEnd();
    sr.Close();

    return results;
}

public class CityStatsOverview
{
    public List<CityStats> CityStats { get; set; }
    public double AverageAirQuality { get; set; }
    public double AverageWaterPollution { get; set; }
}
public class CityStats
{
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public double AirQuality { get; set; }
    public double WaterQuality { get; set; }
}

