using Client.Domain.Repositories.Store;
using Client.Models.Weather;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.Configuration;

namespace Client.Infrastructure.Repositories.Store;

public class WeatherRepository : IWeatherRepository
{
    private readonly Uri _api;
    private readonly IRestRepository _restRepository;

    public WeatherRepository(IConfiguration configuration,
                             IRestRepository restRepository)
    {
        _api = new Uri(string.Concat(configuration["API:Url"],configuration["API:Version"]));
        _restRepository = restRepository;
    }

    public async Task<WeatherSummary> GetByFilter(int skip, int? limit, Guid[]? ids = null, Guid[]? cityIds = null,
                                                  Guid[]? forecastDateIds = null)
    {
        var relativeUri = UrlAdaptor.GetWeatherLink(skip, limit, ids, cityIds, forecastDateIds);
        return await _restRepository.Get<WeatherSummary>(new Uri(_api, relativeUri));
    }

    public async Task<WeatherModel> GetWeather(Guid id)
    {
        var relativeUri = $"weather/{id}";
        return await _restRepository.Get<WeatherModel>(new Uri(_api, relativeUri));
    }
}