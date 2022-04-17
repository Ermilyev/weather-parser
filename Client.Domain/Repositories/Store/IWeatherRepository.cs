using Client.Models.Weather;

namespace Client.Domain.Repositories.Store;

public interface IWeatherRepository
{
    Task<WeatherSummary> GetByFilter(int skip, int? limit, Guid[]? ids = null, Guid[]? cityIds = null,
                                     Guid[]? forecastDateIds = null);
    Task<WeatherModel> GetWeather(Guid id);
}