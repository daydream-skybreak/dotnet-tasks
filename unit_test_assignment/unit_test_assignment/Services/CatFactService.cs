using System.Text.Json;

namespace unit_test_assignment.Services
{
    public class CatFactService(HttpClient httpClient)
    {
        public async Task<string> GetRandomCatFactAsync()
        {
            var response = await httpClient.GetAsync("https://catfact.ninja/fact");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("fact").GetString()!;
        }
    }
}