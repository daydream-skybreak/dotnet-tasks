class Program
{
    HttpClient _httpClient = new HttpClient();
    async Task<string> FetchDataAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    
    async Task ProcessDataAsync(string url)
    {
        try
        {
            string data = await FetchDataAsync(url);
            Console.WriteLine($"Data fetched from {url}: {data.Substring(0, Math.Min(100, data.Length))}...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data from {url}: {ex.Message}");
        }
    }
}