using System.ComponentModel.DataAnnotations;
using WebApi.Models.Weathers;

namespace WebApi.Models.ForecastDates;

public record ForecastDateModel(Guid Id, DateTime Date)
{
    /// <summary>
    /// The id of the forecast date
    /// </summary>
    [Required] public Guid Id { get; init; } = Id;
    /// <summary>
    /// The date of the forecast date
    /// </summary>
    /// <example>07.05.1995</example>
    [Required] public DateTime Date { get; init; } = Date;
}