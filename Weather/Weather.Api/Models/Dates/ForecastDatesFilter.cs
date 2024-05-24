namespace Weather.Api.Models.Dates;

public sealed class ForecastDatesFilter : EntitiesFilter
{
    public HashSet<DateTime>? Days { get; init; }
}