using System.Net;
using System.Net.Http;
using Xunit;
using RichardSzalay.MockHttp;
using unit_test_assignment.Services;

namespace unit_test_assignment.Tests;

public class CatFactServiceTest
{
    [Fact]
    public async Task GetRandomCatFactAsync_ReturnsFact()
    {
        string fakeJson = "{\"fact\": \"Cats sleep 70% of their lives\", \"length\": 32}";

        var mockHttp = new MockHttpMessageHandler();

        mockHttp.When("https://catfact.ninja/fact")
            .Respond("application/json", fakeJson);

        var httpClient = new HttpClient(mockHttp);
        var service = new CatFactService(httpClient);

        var fact = await service.GetRandomCatFactAsync();

        Assert.Equal("Cats sleep 70% of their lives", fact);
    }
}