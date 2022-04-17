using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Domain.Repositories.Store;
using Client.Models.Weather;
using Serilog;

namespace Client.Services.Controllers;

public class WeatherController
{
    private readonly ILogger _logger;
    private readonly IWeatherRepository _weatherRepository;
    
    public WeatherController(ILogger logger,
        IWeatherRepository weatherRepository)
    {
        _logger = logger;
        _weatherRepository = weatherRepository;
    }

    public async Task<WeatherSummary> GetByFilter(int skip, int? limit, Guid[]? ids = null, Guid[]? cityIds = null,
                                                               Guid[]? forecastDateIds = null)
    {
        return await _weatherRepository.GetByFilter(skip, limit, ids, cityIds, forecastDateIds);
    }
    
    public async Task<WeatherModel> GetWeather(Guid id)
    {
        return await _weatherRepository.GetWeather(id);
    }
}