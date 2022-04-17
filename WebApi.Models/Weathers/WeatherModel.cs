using System.ComponentModel.DataAnnotations;
using WebApi.Models.Cities;
using WebApi.Models.ForecastDates;

namespace WebApi.Models.Weathers;

public record WeatherModel(Guid Id, Guid CityId, Guid ForecastDateId, DateTime ParsedAt, short MinTempCelsius,
                           short MinTempFahrenheit, short MaxTempCelsius, short MaxTempFahrenheit,
                           ushort MaxWindSpeedMetersPerSecond, ushort MaxWindSpeedMilesPerHour)
{
    /// <summary>
    /// The weather Id
    /// </summary>
    [Required] public Guid Id { get; init; } = Id;
    /// <summary>
    /// The City Id
    /// </summary>
    [Required] public Guid CityId { get; init; } = CityId;
    /// <summary>
    /// The Forecast Date Id
    /// </summary>
    [Required] public Guid ForecastDateId { get; init; } = ForecastDateId;
    /// <summary>
    /// The Parsed date of the Weather
    /// </summary>
    /// <example>07.05.1995</example>
    [Required] public DateTime ParsedAt { get; init; } = ParsedAt;
    /// <summary>
    /// The Mininimal temperature celsius
    /// </summary>
    ///  <example>0</example>
    [Required] public short MinTempCelsius { get; init; } = MinTempCelsius;
    /// <summary>
    /// The Mininimal temperature Fahrenheit
    /// </summary>
    ///  <example>0</example>
    [Required] public short MinTempFahrenheit { get; init; } = MinTempFahrenheit;
    /// <summary>
    /// The Maximal temperature celsius
    /// </summary>
    ///  <example>0</example>
    [Required] public short MaxTempCelsius { get; init; } = MaxTempCelsius;
    /// <summary>
    /// The Maximal temperature Fahrenheit
    /// </summary>
    ///  <example>0</example>
    [Required] public short MaxTempFahrenheit { get; init; } = MaxTempFahrenheit;
    /// <summary>
    /// The Maximal wind speed miles per second
    /// </summary>
    ///  <example>0</example>
    [Required] public ushort MaxWindSpeedMetersPerSecond { get; init; } = MaxWindSpeedMetersPerSecond;
    /// <summary>
    /// The Maximal wind speed miles per hour
    /// </summary>
    ///  <example>0</example>
    [Required] public ushort MaxWindSpeedMilesPerHour { get; init; } = MaxWindSpeedMilesPerHour;
}
