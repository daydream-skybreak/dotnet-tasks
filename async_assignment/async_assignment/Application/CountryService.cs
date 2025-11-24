using async_assignment.Domain.Interfaces;
using async_assignment.Infrastructure.APIClients.DTOs;

namespace async_assignment.Application;

public class CountryService(
    IApiClient apiCountries,
    IRestCountriesClient restCountries)
    : ICountryService
{
    public async Task<CountryInfo> GetCountryAsync(string countryName)
    {
        // Run both requests concurrently
        var apiCountriesTask = apiCountries.GetCountryAsync(countryName);
        var restCountriesTask = restCountries.GetCountryAsync(countryName);

        await Task.WhenAll(apiCountriesTask, restCountriesTask);

        var apiResult = apiCountriesTask.Result;
        var restResult = restCountriesTask.Result;

        // now combine
        return MergeCountryData(apiResult, restResult);
    }

    private CountryInfo MergeCountryData(ApiCountryDto? a, RestCountryDto? b)
    {
        return new CountryInfo
        {
            Name = a?.Name ?? b?.Name?.Common,
            OfficialName = b?.Name?.Official ?? a?.NativeName,
            Capital = a?.Capital ?? b?.Capital?.FirstOrDefault(),

            Region = a?.Region ?? b?.Region,
            Subregion = a?.Subregion ?? b?.Subregion,

            Population = a?.Population ?? b?.Population ?? 0,
            Area = a?.Area ?? b?.Area,

            Languages = a?.Languages?.ToDictionary(
                            x => x.Iso639_1 ?? x.Iso639_2, 
                            x => x.Name)
                        ?? b?.Languages
                        ?? new Dictionary<string, string>(),

            Currencies = a?.Currencies?.ToDictionary(
                             c => c.Code,
                             c => new CurrencyInfo
                             {
                                 Name = c.Name,
                                 Symbol = c.Symbol
                             })
                         ?? b?.Currencies?.ToDictionary(
                             c => c.Key,
                             c => new CurrencyInfo
                             {
                                 Name = c.Value.Name,
                                 Symbol = c.Value.Symbol
                             })
                         ?? new Dictionary<string, CurrencyInfo>(),

            Flags = new FlagInfo
            {
                PngUrl = a?.Flags?.Png ?? b?.Flags?.Png,
                SvgUrl = a?.Flags?.Svg ?? b?.Flags?.Svg
            }
        };
    }
}
