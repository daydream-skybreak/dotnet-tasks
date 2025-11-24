using async_assignment.Domain.Interfaces;
using async_assignment.Infrastructure.APIClients.DTOs;
using async_assignment.Infrastructure.Http;
using Microsoft.Extensions.DependencyInjection;

namespace async_assignment.Infrastructure.APIClients;

public class RestCountryClient(HttpClient http) : IRestCountriesClient
{
    public async Task<RestCountryDto?> GetCountryAsync(string name, CancellationToken ct = default)
    {
        var response = await http.GetAsync($"/name/{name}", ct);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var content = await response.Content.ReadAsStringAsync(ct);
        var countries = System.Text.Json.JsonSerializer.Deserialize<List<RestCountryDto>>(content, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return countries?.FirstOrDefault();
    }
}