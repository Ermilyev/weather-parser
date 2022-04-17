using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Parser.Models.Weathers.Aggregates;

namespace Parser.Models.Weathers;

public record WeatherModel()
{
    [Required][JsonProperty("id")] public Guid Id { get; set; }
    [Required][JsonProperty("cityId")] public Guid CityId { get; set; }
    [Required][JsonProperty("forecastDateId")] public Guid ForecastDateId { get; set; }
    [Required][JsonProperty("parsedAt")] public DateTime ParsedAt { get; set; }
    [Required][JsonProperty("minTempCelsius")] public short MinTempCelsius { get; set; }
    [Required][JsonProperty("minTempFahrenheit")] public short MinTempFahrenheit { get; set; }
    [Required][JsonProperty("maxTempCelsius")] public short MaxTempCelsius { get; set; }
    [Required][JsonProperty("maxTempFahrenheit")] public short MaxTempFahrenheit { get; set; }
    [Required][JsonProperty("maxWindSpeedMetersPerSecond")] public ushort MaxWindSpeedMetersPerSecond { get; set; }
    [Required][JsonProperty("maxWindSpeedMilesPerHour")] public ushort MaxWindSpeedMilesPerHour { get; set; }
    public WeatherPart Part() => new(Id, CityId, ForecastDateId);
}