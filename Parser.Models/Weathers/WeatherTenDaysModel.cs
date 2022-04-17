namespace Parser.Models.Weathers;

public record WeatherTenDaysModel(string CityName,
                                  DateTime ParsedAt, IEnumerable<WeatherDailyModel> WeatherItems)
{
    public string CityName { get; set; } = CityName;
    public DateTime ParsedAt { get; set; } = ParsedAt;
    public IEnumerable<WeatherDailyModel> WeatherItems { get; set; } = WeatherItems;
    public IEnumerable<DateTime> ForecastDates() => WeatherItems.Select(w => w.ForecastDate);
}