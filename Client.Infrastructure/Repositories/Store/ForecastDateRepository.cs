using Client.Domain.Repositories.Store;
using Client.Models.ForecastDate;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.Configuration;

namespace Client.Infrastructure.Repositories.Store;

public class ForecastDateRepository : IForecastDateRepository
{
    private readonly Uri _api;
    private readonly IRestRepository _restRepository;

    public ForecastDateRepository(IConfiguration configuration,
                                  IRestRepository restRepository)
    {
        _api = new Uri(string.Concat(configuration["API:Url"],configuration["API:Version"]));
        _restRepository = restRepository;
    }

    public async Task<IReadOnlyList<ForecastDateModel>> GetByFilter(DateTime[]? dates)
    {
        var relativeUri = $"forecastdate?date={dates.First()}";
        return await _restRepository.Get<List<ForecastDateModel>>(new Uri(_api, relativeUri));
    }
    
    public async Task<ForecastDateModel> GetForecastDate(Guid id)
    {
        var relativeUri = $"forecastdate/{id}";
        return await _restRepository.Get<ForecastDateModel>(new Uri(_api, relativeUri));
    }
}