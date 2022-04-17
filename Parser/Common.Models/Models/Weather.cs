using Common.Models.Entities;

namespace Common.Models.Models;

public class Weather : Entity
{
    public Guid CityId { get; set; }
    public Guid ForecastDateId { get; set; }
    public DateTime ParsedAt { get; set; }
    public short MinTempCelsius { get; set; }
    public short MinTempFahrenheit { get; set; }
    public short MaxTempCelsius { get; set; }
    public short MaxTempFahrenheit { get; set; }
    public ushort MaxWindSpeedMetersPerSecond { get; set; }
    public ushort MaxWindSpeedMilesPerHour { get; set; }
    public City City { get; set; }
    public ForecastDate ForecastDate { get; set; }
}