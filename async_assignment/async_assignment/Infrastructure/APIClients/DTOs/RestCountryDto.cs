namespace async_assignment.Infrastructure.APIClients.DTOs;

public class RestCountryDto
{
    public NameDto Name { get; set; }
    public List<string> Tld { get; set; }
    public string Cca2 { get; set; }
    public string Cca3 { get; set; }
    public List<string> Capital { get; set; }
    public string Region { get; set; }
    public string Subregion { get; set; }
    public long Population { get; set; }
    public double? Area { get; set; }
    public List<double> Latlng { get; set; }
    public bool? Independent { get; set; }
    public List<string> Timezones { get; set; }
    public List<string> Borders { get; set; }

    public Dictionary<string, CurrencyDetailDto> Currencies { get; set; }

    public Dictionary<string, string> Languages { get; set; }

    public FlagsRestDto Flags { get; set; }

    public Dictionary<string, TranslationDto> Translations { get; set; }
}

public class NameDto
{
    public string Common { get; set; }
    public string Official { get; set; }
    public Dictionary<string, NativeNameDto> NativeName { get; set; }
}

public class NativeNameDto
{
    public string Official { get; set; }
    public string Common { get; set; }
}

public class CurrencyDetailDto
{
    public string Name { get; set; }
    public string Symbol { get; set; }
}

public class FlagsRestDto
{
    public string Png { get; set; }
    public string Svg { get; set; }
}

public class TranslationDto
{
    public string Official { get; set; }
    public string Common { get; set; }
}
