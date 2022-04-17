using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.ForecastDates;

public record CreateForecastDateModel(DateTime Date)
{
    /// <summary>
    /// The date of the forecast date
    /// </summary>
    /// <example>07.05.1995</example>
    [Required] public DateTime Date { get; init; } = Date;
}