using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Parser.Models.Weathers;

public record CreateWeatherModel(Guid CityId, Guid ForecastDateId, DateTime ParsedAt, short MinTempCelsius,
    short MinTempFahrenheit, short MaxTempCelsius, short MaxTempFahrenheit,
    ushort MaxWindSpeedMetersPerSecond, ushort MaxWindSpeedMilesPerHour)
{
    [Required][JsonProperty("cityId")] public Guid CityId { get; set; } = CityId;
    [Required][JsonProperty("forecastDateId")] public Guid ForecastDateId { get; set; } = ForecastDateId;
    [JsonProperty("parsedAt")] public DateTime ParsedAt { get; set; } = ParsedAt;
    [JsonProperty("minTempCelsius")] public short MinTempCelsius { get; set; } = MinTempCelsius;
    [JsonProperty("minTempFahrenheit")] public short MinTempFahrenheit { get; set; } = MinTempFahrenheit;
    [JsonProperty("maxTempCelsius")] public short MaxTempCelsius { get; set; } = MaxTempCelsius;
    [JsonProperty("maxTempFahrenheit")] public short MaxTempFahrenheit { get; set; } = MaxTempFahrenheit;
    [JsonProperty("maxWindSpeedMetersPerSecond")] public ushort MaxWindSpeedMetersPerSecond { get; set; } = MaxWindSpeedMetersPerSecond;
    [JsonProperty("maxWindSpeedMilesPerHour")] public ushort MaxWindSpeedMilesPerHour { get; set; } = MaxWindSpeedMilesPerHour;
}