using async_assignment.Infrastructure.APIClients.DTOs;

namespace async_assignment.Domain.Interfaces;

public interface IApiClient
{
    Task<ApiCountryDto?> GetCountryAsync(string name, CancellationToken ct = default);
}