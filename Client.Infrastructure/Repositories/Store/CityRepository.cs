using Client.Domain.Repositories.Store;
using Client.Models.City;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.Configuration;

namespace Client.Infrastructure.Repositories.Store;

public class CityRepository : ICityRepository
{
    private readonly Uri _api;
    private readonly IRestRepository _restRepository;

    public CityRepository(IConfiguration configuration, 
                          IRestRepository restRepository)
    {
        _api = new Uri(string.Concat(configuration["API:Url"],configuration["API:Version"]));
        _restRepository = restRepository;
    }

    public async Task<CitySummary> GetByFilter(Guid[]? ids = null, string[]? names = null)
    {
        var relativeUri = UrlAdaptor.GetCityLink(ids, names);
        return await _restRepository.Get<CitySummary>(new Uri(_api, relativeUri));
    }

    public async Task<CityModel> GetCity(Guid id)
    {
        var relativeUri = $"city/{id}";
        return await _restRepository.Get<CityModel>(new Uri(_api, relativeUri));
    }
}