using async_assignment.Infrastructure.APIClients.DTOs;

namespace async_assignment.Domain.Interfaces;

public interface IRestCountriesClient
{
    Task<RestCountryDto?> GetCountryAsync(string name, CancellationToken ct = default);
}