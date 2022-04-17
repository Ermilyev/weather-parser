namespace WebApi.Models.ForecastDates;

public record UpdateForecastDateModel(DateTime Date)
{
    /// <summary>
    /// The date of the forecast date
    /// </summary>
    /// <example>07.05.1995</example>
    public DateTime Date { get; init; } = Date;
}