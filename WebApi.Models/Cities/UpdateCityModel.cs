namespace WebApi.Models.Cities;

public record UpdateCityModel(string Name)
{
    /// <summary>
    /// The name of the city
    /// </summary>
    ///  <example>Санкт-Петербург</example>
    public string Name { get; init; } = Name;
}