public class CountryInfo
{
    public string? Name { get; set; }
    public string? OfficialName { get; set; }
    public string? Capital { get; set; }

    public string? Region { get; set; }
    public string? Subregion { get; set; }

    public long Population { get; set; }
    public double? Area { get; set; }

    public Dictionary<string, string> Languages { get; set; }
    public Dictionary<string, CurrencyInfo> Currencies { get; set; }

    public FlagInfo Flags { get; set; }
}

public class CurrencyInfo
{
    public string Name { get; set; }
    public string Symbol { get; set; }
}

public class FlagInfo
{
    public string? PngUrl { get; set; }
    public string? SvgUrl { get; set; }
}