namespace Weather.Api.Models.Dates;

public sealed class UpdateForecastDateModel(DateTime? day)
{
    public DateTime? Day { get; init; } = day;
}