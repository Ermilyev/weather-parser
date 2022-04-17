using System.ComponentModel.DataAnnotations;

namespace Client.Models.Weather;

public record WeatherModel
{
    public Guid Id { get; set; }
    public Guid CityId { get; set; }
    public Guid ForecastDateId { get; set; }
    [Required] public DateTime ParsedAt { get; set; }
    [Required] public short MinTempCelsius { get; set; }
    [Required] public short MinTempFahrenheit { get; set; }
    [Required] public short MaxTempCelsius { get; set; }
    [Required] public short MaxTempFahrenheit { get; set; }
    [Required] public ushort MaxWindSpeedMetersPerSecond { get; set; }
    [Required] public ushort MaxWindSpeedMilesPerHour { get; set; }
}