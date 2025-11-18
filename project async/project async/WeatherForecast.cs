using System.Runtime.ExceptionServices;

namespace project_async;

public class WeatherForecast
{
    static async Task Main(string[] args)
    {
        var weatherApis = new WeatherApis();
        var semaphore = new SemaphoreSlim(3);
        var cities = new List<string>()
        {
            "London", "New York", "Sydney", "Tokyo", "Mumbai", "Cairo", "Moscow", "Rio de Janeiro", "Toronto", "Beijing"
        };
        var tasks = new List<Task>();

        foreach (var item in cities)
        {
            await semaphore.WaitAsync();
            var task = Task.Run(async () =>
                {
                    try
                    {
                        await weatherApis.GetWeatherForecast(item);
                    }
                    catch (AggregateException ex)
                    {
                        foreach (var inner in ex.InnerExceptions)
                            Console.WriteLine($"Failed to get weather forecast for {item}: {inner.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            $"an exception occurred while calling on weather api for {item}: {ex.Message}");
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }
            );
            
            tasks.Add(task);
        }
        await Task.WhenAll(tasks);
        Console.WriteLine("All weather forecasts have been processed.");
        semaphore.Dispose();
    }
}