using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.ForecastDates.Aggregates;

public record ForecastDateFilter([FromQuery(Name = "ids")] HashSet<Guid>? Ids = null,
                                 [FromQuery(Name = "dates")] HashSet<DateTime>? Dates = null)
{
    /// <summary>
    /// The date set of the forecast date
    /// </summary>
    public HashSet<DateTime>? Dates { get; init; } = Dates;
}