using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Cities.Aggregates;

public record CityFilter([FromQuery(Name = "ids")] HashSet<Guid>? Ids = null,
                         [FromQuery(Name = "names")] HashSet<string>? Names = null)
{
    /// <summary>
    /// The Id set of the city
    /// </summary>
    public HashSet<Guid>? Ids { get; init; } = Ids;
    /// <summary>
    /// The name set of the city
    /// </summary>
    public HashSet<string>? Names { get; init; } = Names;
}