namespace Parser.Models.Weathers;

public record WeatherDailyModel()
{
    public DateTime ForecastDate { get; set; }
    public short MinTempCelsius{ get; set; }
    public short MinTempFahrenheit{ get; set; }
    public short MaxTempCelsius { get; set; }
    public short MaxTempFahrenheit{ get; set; }
    public ushort MaxWindSpeedMetersPerSecond { get; set; }
    public ushort MaxWindSpeedMilesPerHour { get; set; }
}