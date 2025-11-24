using async_assignment.Application;
using async_assignment.Infrastructure.APIClients;
using async_assignment.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddHttpClient<IApiClient, ApiCountryClient>(c =>
        {
            c.BaseAddress = new Uri("https://api.api-ninjas.com/v1/");
        });

        services.AddHttpClient<IRestCountriesClient, RestCountryClient>(c =>
        {
            c.BaseAddress = new Uri("https://restcountries.com/v3.1/");
        });

        services.AddScoped<CountryService>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var svc = scope.ServiceProvider.GetRequiredService<CountryService>();

var countries = new List<string>
{
    "United States",
    "Japan",
    "Germany",
    "Brazil",
    "Canada",
    "India",
    "China",
    "Australia",
    "France",
    "South Africa"
};

Console.WriteLine("Choose a country from the list below by entering the number (1-10) or typing the country name:");
for (int i = 0; i < countries.Count; i++)
{
    Console.WriteLine($"{i + 1}. {countries[i]}");
}

Console.Write("Your choice: ");
var input = Console.ReadLine()?.Trim();
if (string.IsNullOrWhiteSpace(input))
{
    Console.WriteLine("No input provided. Exiting.");
    return;
}

string selectedName;
if (int.TryParse(input, out var idx) && idx >= 1 && idx <= countries.Count)
{
    selectedName = countries[idx - 1];
}
else
{
    selectedName = input;
}

Console.WriteLine($"Fetching data for '{selectedName}'...\n");

try
{
    var result = await svc.GetCountryAsync(selectedName);
    var opts = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
    var json = JsonSerializer.Serialize(result, opts);
    Console.WriteLine(json);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred while fetching country data: {ex.Message}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey(true);
