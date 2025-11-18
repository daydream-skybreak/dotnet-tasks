using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace project_async;
public class DummyWeatherApis
{
    private static readonly Random _rand = new Random();

    public async Task<double?> GetTemperatureFromApiAAsync(string city, CancellationToken token)
    {
        await Task.Delay(_rand.Next(500, 2500), token);
        try
        {
            await Task.Delay(_rand.Next(400, 2000), token);
            if (_rand.NextDouble() < 0.2 )
                throw new TimeoutException($"API A timeout for city {city}.");
            return Math.Round(_rand.NextDouble() * 40, 1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<double?> GetTemperatureFromApiBAsync(string city, CancellationToken token)
    {
        await Task.Delay(_rand.Next(800, 3000), token);
        try
        {
            await Task.Delay(_rand.Next(400, 2000), token);
            if (_rand.NextDouble() < 0.3)
                throw new TimeoutException($"API B timeout. for city {city}");
            return Math.Round(_rand.NextDouble() * 40, 1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<double?> GetTemperatureFromApiCAsync(string city, CancellationToken token)
    {
        try
        {
            await Task.Delay(_rand.Next(400, 2000), token);
            if (_rand.NextDouble() < 0.1)
                throw new TimeoutException($"API C timeout. for city {city}");
            return Math.Round(_rand.NextDouble() * 40, 1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}