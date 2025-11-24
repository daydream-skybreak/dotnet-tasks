namespace async_assignment.Domain.Interfaces;

public interface ICountryService
{
    Task<CountryInfo> GetCountryAsync(string countryName);
}