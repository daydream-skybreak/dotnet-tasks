namespace async_assignment.Infrastructure.Http;
using Microsoft.Extensions.DependencyInjection;

public static class HttpClientFactorySetup
{
    public static IServiceCollection AddHttpClientFactory(this IServiceCollection services)
    {
        services.AddHttpClient("ApiCountries", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            client.BaseAddress = new Uri("https://www.apicountries.com/");
        });
        services.AddHttpClient("RestCountries", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(10);
            client.BaseAddress = new Uri("https://restcountries.com/v3.1/");
        });

        return services;
    }
    
}