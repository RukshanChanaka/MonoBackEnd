using Microsoft.AspNetCore.Mvc;
using ACEBackEnd.DataAccess;

namespace ACEBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorchingg"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DataAccessService _dataAccess;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DataAccessService dataAccess)
    {
        _logger = logger;
        _dataAccess = dataAccess;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        string myAppKey = Environment.GetEnvironmentVariable("ENV-VAR");
        //var data =  await _dataAccess.GetDataAsync();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length - 1)] + myAppKey
        })
        .ToArray();
    }
}
