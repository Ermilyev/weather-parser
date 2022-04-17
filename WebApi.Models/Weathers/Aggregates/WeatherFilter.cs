using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Weathers.Aggregates;

public record WeatherFilter([FromQuery(Name = "ids")] HashSet<Guid>? Ids = null,
                            [FromQuery(Name = "cityIds")] HashSet<Guid>? CityIds = null,
                            [FromQuery(Name = "forecastDateIds")] HashSet<Guid>? ForecastDateIds = null)   
{
    /// <summary>
    /// The Id set of weathers
    /// </summary>
    public HashSet<Guid>? Ids { get; init; } = Ids;
    /// <summary>
    /// The city Id set of weathers
    /// </summary>
    public HashSet<Guid>? CityIds { get; init; } = CityIds;
    /// <summary>
    /// The date Id set of weathers
    /// </summary>
    public HashSet<Guid>? ForecastDateIds { get; init; } = ForecastDateIds;
}