namespace async_assignment.Infrastructure.APIClients.DTOs;

public class ApiCountryDto
{
    public string Name { get; set; }
    public List<string> TopLevelDomain { get; set; }
    public string Alpha2Code { get; set; }
    public string Alpha3Code { get; set; }
    public List<string> CallingCodes { get; set; }
    public string Capital { get; set; }
    public List<string> AltSpellings { get; set; }
    public string Subregion { get; set; }
    public string Region { get; set; }
    public long Population { get; set; }
    public List<double> Latlng { get; set; }
    public string Demonym { get; set; }
    public double? Area { get; set; }
    public List<string> Timezones { get; set; }
    public List<string> Borders { get; set; }
    public string NativeName { get; set; }
    public string NumericCode { get; set; }

    public FlagsDto Flags { get; set; }

    public List<CurrencyDto> Currencies { get; set; }

    public List<LanguageDto> Languages { get; set; }

    public Dictionary<string, string> Translations { get; set; }

    public string Flag { get; set; }  // Sometimes they include a flat flag url

    public List<RegionalBlocDto> RegionalBlocs { get; set; }

    public string Cioc { get; set; }

    public bool Independent { get; set; }
}

public class FlagsDto
{
    public string Svg { get; set; }
    public string Png { get; set; }
}

public class CurrencyDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
}

public class LanguageDto
{
    public string Iso639_1 { get; set; }
    public string Iso639_2 { get; set; }
    public string Name { get; set; }
    public string NativeName { get; set; }
}

public class RegionalBlocDto
{
    public string Acronym { get; set; }
    public string Name { get; set; }
}
