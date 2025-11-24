using async_assignment.Domain.Interfaces;
using async_assignment.Infrastructure.APIClients.DTOs;

namespace async_assignment.Infrastructure.APIClients;

public class ApiCountryClient(HttpClient http) : IApiClient
{
    private readonly HttpClient _http = http;
    
    public async Task<ApiCountryDto?> GetCountryAsync(string name, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"/name/{name}", ct);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var content = await response.Content.ReadAsStringAsync(ct);
        var countries = System.Text.Json.JsonSerializer.Deserialize<List<ApiCountryDto>>(content, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return countries?.FirstOrDefault();
    }
}