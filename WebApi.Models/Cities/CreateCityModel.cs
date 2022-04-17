using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Cities;

public record CreateCityModel(string Name)
{
    /// <summary>
    /// The name of the city
    /// </summary>
    /// <example>Санкт-Петербург</example>
    [Required] public string Name { get; init; } = Name;
}