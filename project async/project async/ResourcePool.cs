using System.Collections.Concurrent;
using System.Reflection;
using System.Security.Authentication;

namespace project_async;

public class WeatherApis
{
    private static DummyWeatherApis _weatherApis;
    public WeatherApis()
    {
        _weatherApis = new DummyWeatherApis();
    }

    public async Task GetWeatherForecast(string city)
    {
        Console.WriteLine($"the program has started for {city}");
        
        var result = new List<Task<double?>>()
        {
            Task.Run(() => _weatherApis.GetTemperatureFromApiAAsync(city, CancellationToken.None).Result),
            Task.Run(() => _weatherApis.GetTemperatureFromApiBAsync(city, CancellationToken.None).Result),
            Task.Run(() => _weatherApis.GetTemperatureFromApiCAsync(city, CancellationToken.None).Result),
        };
        var weatherResults = await Task.WhenAll(result);
        var validResults = weatherResults.Where(temp => temp.HasValue).Select(temp => temp.Value).ToList();
        if (validResults.Any())
        {
            var averageTemp = validResults.Average();
            Console.WriteLine($"The average temperature in {city} is {averageTemp}°C based on {validResults.Count} valid readings.");
        }
        else
        {
            Console.WriteLine($"All API calls failed for {city}. Unable to retrieve temperature data.");
        }
    }
}