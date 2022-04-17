using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Weathers;

public record UpdateWeatherModel(Guid CityId, Guid ForecastDateId, DateTime ParsedAt, short MinTempCelsius,
    short MinTempFahrenheit, short MaxTempCelsius, short MaxTempFahrenheit,
    ushort MaxWindSpeedMetersPerSecond, ushort MaxWindSpeedMilesPerHour)
{
    /// <summary>
    /// The City Id
    /// </summary>
    public Guid CityId { get; init; } = CityId;
    /// <summary>
    /// The Forecast Date Id
    /// </summary>
    public Guid ForecastDateId { get; init; } = ForecastDateId;
    /// <summary>
    /// The Parsed date of the Weather
    /// </summary>
    /// <example>07.05.1995</example>
    public DateTime ParsedAt { get; init; } = ParsedAt;
    /// <summary>
    /// The Mininimal temperature celsius
    /// </summary>
    /// <example>0</example>
    public short MinTempCelsius { get; init; } = MinTempCelsius;
    /// <summary>
    /// The Mininimal temperature Fahrenheit
    /// </summary>
    /// <example>0</example>
    public short MinTempFahrenheit { get; init; } = MinTempFahrenheit;
    /// <summary>
    /// The Maximal temperature celsius
    /// </summary>
    /// <example>0</example>
    public short MaxTempCelsius { get; init; } = MaxTempCelsius;
    /// <summary>
    /// The Maximal temperature Fahrenheit
    /// </summary>
    /// <example>0</example>
    public short MaxTempFahrenheit { get; init; } = MaxTempFahrenheit;
    /// <summary>
    /// The Maximal wind speed miles per second
    /// </summary>
    /// <example>0</example>
    public ushort MaxWindSpeedMetersPerSecond { get; init; } = MaxWindSpeedMetersPerSecond;
    /// <summary>
    /// The Maximal wind speed miles per hour
    /// </summary>
    /// <example>0</example>
    public ushort MaxWindSpeedMilesPerHour { get; init; } = MaxWindSpeedMilesPerHour;
}