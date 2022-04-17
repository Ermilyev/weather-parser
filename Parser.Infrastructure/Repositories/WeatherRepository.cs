using HtmlAgilityPack;
using Parser.Domain.Repositories;
using Parser.Models.Cities;
using Parser.Models.Weathers;

namespace Parser.Infrastructure.Repositories;

public class WeatherRepository : IWeatherRepository
{
    public async Task<List<WeatherTenDaysModel>> ParseAsync(List<CityToParse> cityModelList)
    {
        var weatherItemList = new List<WeatherTenDaysModel>();
        var parsedAt = DateTime.Today;

        if (!cityModelList.Any()) 
            return weatherItemList;
        
        foreach (var (name, path) in cityModelList)
        {
            var weatherItem = await ParseCityWeatherPointsAsync(path, parsedAt);
            weatherItemList.Add(new WeatherTenDaysModel(name, parsedAt,weatherItem));
        }

        return weatherItemList;
    }

    private static async Task<List<WeatherDailyModel>> ParseCityWeatherPointsAsync(string path, DateTime parsedAt)
    {
        var cityWeatherPoints = new List<WeatherDailyModel>();

        if (string.IsNullOrWhiteSpace(path))
            return cityWeatherPoints;

        var htmlWeb = new HtmlWeb();
        var htmlDocument = htmlWeb.Load(path);

        for (var i = 0; i < 10; i++)
        {
            cityWeatherPoints.Add(new WeatherDailyModel
            {
                ForecastDate = parsedAt.AddDays(i)
            });
        }

        cityWeatherPoints = await ParseTemperaturesAsync(htmlDocument, cityWeatherPoints);
        cityWeatherPoints = await ParseWindSpeedsAsync(htmlDocument, cityWeatherPoints);
        
        return cityWeatherPoints;
    }

    private static async Task<List<WeatherDailyModel>> ParseTemperaturesAsync(HtmlDocument htmlDocument,
        List<WeatherDailyModel> cityWeatherPoints)
    {
        await Task.Run(() =>
        {
            var temperaturesDivNode = htmlDocument.DocumentNode.SelectSingleNode(
                "//div[@class='chart ten-days']/div[@class='values']");

            for (var i = 0; i < 10; i++)
            {
                var maxTempCelsiusSpanNode = temperaturesDivNode.SelectSingleNode(
                    $"div[{i + 1}]/div[@class='maxt']/span[@class='unit unit_temperature_c']");
                if (maxTempCelsiusSpanNode is not null)
                    cityWeatherPoints[i].MaxTempCelsius =
                        sbyte.Parse(maxTempCelsiusSpanNode.InnerText.HtmlDecode());

                var maxTempFahrenheitSpanNode = temperaturesDivNode.SelectSingleNode(
                    $"div[{i + 1}]/div[@class='maxt']/span[@class='unit unit_temperature_f']");
                if (maxTempFahrenheitSpanNode is not null)
                    cityWeatherPoints[i].MaxTempFahrenheit =
                        sbyte.Parse(maxTempFahrenheitSpanNode.InnerText.HtmlDecode());

                var minTempCelsiusSpanNode = temperaturesDivNode.SelectSingleNode(
                    $"div[{i + 1}]/div[@class='mint']/span[@class='unit unit_temperature_c']");
                if (minTempCelsiusSpanNode is not null)
                    cityWeatherPoints[i].MinTempCelsius =
                        sbyte.Parse(minTempCelsiusSpanNode.InnerText.HtmlDecode());

                var minTempFahrenheitSpanNode = temperaturesDivNode.SelectSingleNode(
                    $"div[{i + 1}]/div[@class='mint']/span[@class='unit unit_temperature_f']");
                if (minTempFahrenheitSpanNode is not null)
                    cityWeatherPoints[i].MinTempFahrenheit =
                        sbyte.Parse(minTempFahrenheitSpanNode.InnerText.HtmlDecode());
            }
        });
        
        return cityWeatherPoints;
    }

    private static async Task<List<WeatherDailyModel>> ParseWindSpeedsAsync(HtmlDocument htmlDocument,
        List<WeatherDailyModel> cityWeatherPoints)
    {
        await Task.Run(() =>
        {
            var windSpeedsDivNode = htmlDocument.DocumentNode.SelectSingleNode(
                "//div[@class='widget-row widget-row-wind-gust row-with-caption']");

            for (var i = 0; i < 10; i++)
            {
                var maxWindSpeedMetersPerSecondSpanNode =
                    windSpeedsDivNode.SelectSingleNode($"div[{i + 1}]/span[@class='wind-unit unit unit_wind_m_s']");
                if (maxWindSpeedMetersPerSecondSpanNode is not null)
                    cityWeatherPoints[i].MaxWindSpeedMetersPerSecond =
                        ushort.Parse(maxWindSpeedMetersPerSecondSpanNode.InnerText.HtmlDecode());

                var maxWindSpeedMilesPerHourSpanNode =
                    windSpeedsDivNode.SelectSingleNode($"div[{i + 1}]/span[@class='wind-unit unit unit_wind_km_h']");
                if (maxWindSpeedMilesPerHourSpanNode is not null)
                    cityWeatherPoints[i].MaxWindSpeedMilesPerHour =
                        ushort.Parse(maxWindSpeedMilesPerHourSpanNode.InnerText.HtmlDecode());
            }
        });

        return cityWeatherPoints;
    }
}