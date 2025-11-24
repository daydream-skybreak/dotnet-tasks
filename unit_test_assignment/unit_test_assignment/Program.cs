using unit_test_assignment;
using System;
using System.Net.Http;
using unit_test_assignment.Services;

namespace unit_test_assignment;
class Program
{
    static async Task Main()
    {
        var httpClient = new HttpClient();
        var service = new CatFactService(httpClient);

        var fact = await service.GetRandomCatFactAsync();

        Console.WriteLine("Random Cat Fact:");
        Console.WriteLine(fact);
    }
}