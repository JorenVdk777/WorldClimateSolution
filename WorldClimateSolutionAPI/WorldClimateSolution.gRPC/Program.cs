using System.Net;
using WorldClimateSolution.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();
var test = GetCSV("https://github.com/JorenVdk777/WorldClimateSolution/blob/rlaevaert/BackEndAdded/WorldClimateSolutionAPI/WorldClimateSolution.gRPC/cities_air_quality_water_pollution.18-10-2021.csv").Split(',').Skip(1).Select(x => x);


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
