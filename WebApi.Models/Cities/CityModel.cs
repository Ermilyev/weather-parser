using System.ComponentModel.DataAnnotations;
using WebApi.Models.Weathers;

namespace WebApi.Models.Cities;

public record CityModel(Guid Id, string Name)
{
    /// <summary>
    /// The Id of the city
    /// </summary>
    [Required] public  Guid Id { get; init; } = Id;
    /// <summary>
    /// The name of the city
    /// </summary>
    /// <example>Санкт-Петербург</example>
    [Required] public  string Name { get; init; } = Name;
}