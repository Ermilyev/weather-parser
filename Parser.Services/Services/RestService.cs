using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Parser.Models.Cities;
using Parser.Models.ForecastDates;
using Parser.Models.Weathers;
using Parser.Services.Services.RestServices;

namespace Parser.Services.Services;

internal class RestService
{
    private readonly Uri _api;
    private readonly ILogger<RestService> _logger;
    private readonly IRestRepository _restRepository;
    private readonly ForecastDateRestService _forecastDateRestService;
    private readonly CityRestService _cityRestService;
    private readonly WeatherRestService _weatherRestService;

    public RestService(IConfiguration configuration, 
                       ILogger<RestService> logger, 
                       IRestRepository restRepository, 
                       ForecastDateRestService forecastDateRestService, 
                       CityRestService cityRestService,
                       WeatherRestService weatherRestService)
    {
        _api = new Uri(string.Concat(configuration["API:Url"],configuration["API:Version"]));
        _logger = logger;
        _restRepository = restRepository;
        _forecastDateRestService = forecastDateRestService;
        _cityRestService = cityRestService;
        _weatherRestService = weatherRestService;
    }

    internal async Task WorkAsync(List<WeatherTenDaysModel> weatherTenDaysModels)
    {
        try
        {
            _logger.LogInformation(
                "Starting | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                nameof(RestService),
                nameof(WorkAsync),
                DateTime.Now);

            var result = await GetStatusConnection(_api);

            if (result == HttpStatusCode.RequestTimeout)
                return;
            
            var dates = new List<DateTime>();
            weatherTenDaysModels.ForEach(w => dates.AddRange(w.ForecastDates()));
            var datesModelList = await _forecastDateRestService.WorkAsync(dates.ToHashSet());

            if (datesModelList.Any() == false)
                return;
        
            _logger.LogInformation(
                "Processed | Service: {Service}, Method: {Method}, dates count - {Count}, Time - {Datetime}",  
                nameof(RestService),
                nameof(WorkAsync),
                datesModelList.Count,
                DateTime.Now);

            var cityNames = new List<string>();
            weatherTenDaysModels.ForEach(w => cityNames.Add(w.CityName));
            var cityModelList = await _cityRestService.WorkAsync(cityNames.ToHashSet());

            if (cityModelList.Any() == false)
                return;

            var models = From(weatherTenDaysModels, datesModelList, cityModelList);

            if (models.Any() == false)
                return;

            var weatherModelList = await _weatherRestService.WorkAsync(models);
        
            _logger.LogInformation(
                "Finished | Service: {Service}, Method: {Method}, weathers count - {Count},  Time - {Datetime}",  
                nameof(RestService),
                nameof(WorkAsync),
                weatherModelList.Count,
                DateTime.Now);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}",  
                ex.Message,
                nameof(RestService),
                nameof(WorkAsync));
            
            throw;
        }
    }

    private async Task<HttpStatusCode> GetStatusConnection(Uri api)
    {
        try
        {
            var result = await _restRepository.GetStatusConnection(api);
            
            _logger.LogInformation(
                "Service: {Service}, Method: {Method}, Status: {Status}",   
                nameof(RestService),
                nameof(GetStatusConnection),
                result);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}",      
                ex.Message,
                nameof(RestService),
                nameof(GetStatusConnection));
            
            throw;
        }
    }

    private static IEnumerable<WeatherModel> From(IEnumerable<WeatherTenDaysModel> weatherTenDaysModels,
                                                  IEnumerable<ForecastDateModel> forecastDatesModels,
                                                  IEnumerable<CityModel> cityModels)
    {
        var models = (from weatherTenDaysModel in weatherTenDaysModels
                      let cityName = weatherTenDaysModel.CityName
                      let cityId = cityModels.First(c => c.Name == cityName).Id
                      let oneDayModels = weatherTenDaysModel.WeatherItems
                      from oneDayModel in oneDayModels
                      let dateId = forecastDatesModels.First(d => d.Date == oneDayModel.ForecastDate).Id
                      select new WeatherModel()
                      {
                          Id = Guid.Empty,
                          CityId = cityId,
                          ForecastDateId = dateId,
                          ParsedAt = weatherTenDaysModel.ParsedAt,
                          MinTempCelsius = oneDayModel.MinTempCelsius,
                          MinTempFahrenheit = oneDayModel.MinTempFahrenheit,
                          MaxTempCelsius = oneDayModel.MaxTempCelsius,
                          MaxTempFahrenheit = oneDayModel.MaxTempFahrenheit,
                          MaxWindSpeedMetersPerSecond = oneDayModel.MaxWindSpeedMetersPerSecond,
                          MaxWindSpeedMilesPerHour = oneDayModel.MaxWindSpeedMilesPerHour
                      });

        return models;
    }
}