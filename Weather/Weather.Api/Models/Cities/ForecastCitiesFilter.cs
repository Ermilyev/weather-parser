namespace Weather.Api.Models.Cities;

public sealed class ForecastCitiesFilter: EntitiesFilter
{
    public HashSet<string>? Names { get; init; }
}