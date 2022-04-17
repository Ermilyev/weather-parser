using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Parser.Models.Weathers;
using Parser.Models.Weathers.Aggregates;

namespace Parser.Services.Services.RestServices;

public class WeatherRestService
{
    private readonly Uri _api;
    private readonly IMapper _mapper;
    private readonly ILogger<WeatherRestService> _logger;
    private readonly IRestRepository _restRepository;

    public WeatherRestService(IConfiguration configuration, IMapper mapper, ILogger<WeatherRestService> logger, IRestRepository restRepository)
    {
        _api = new Uri(string.Concat(configuration["API:Url"], configuration["API:Version"]));
        _mapper = mapper;
        _logger = logger;
        _restRepository = restRepository;
    }

    public async Task<List<WeatherModel>> WorkAsync(IEnumerable<WeatherModel> models)
    {
        try
        {
            _logger.LogInformation(
                "Starting | Service: {Service}, Method: {Method}, Time - {Datetime}",
                nameof(WeatherRestService),
                nameof(WorkAsync),
                DateTime.Now);

            var existingModels = await ExistAsync(models.ToList());

            if (existingModels.Any())
                models.CopyTo(existingModels);

            foreach (var model in models)
            {
                if (model.Id == Guid.Empty)
                    continue;

                var updateModel = _mapper.Map<UpdateWeatherModel>(model);
                await UpdateAsync(model.Id, updateModel);
            }

            foreach (var model in models)
            {
                if (model.Id != Guid.Empty)
                    continue;

                var createModel = _mapper.Map<CreateWeatherModel>(model);
                var insertedWeatherModel = await InsertAsync(createModel);

                if (insertedWeatherModel.Id == Guid.Empty)
                    continue;

                var weatherModel = models
                    .FirstOrDefault(m => m.CityId == model.CityId && m.ForecastDateId == model.ForecastDateId);

                if (weatherModel is not null)
                    weatherModel.Id = insertedWeatherModel.Id;
            }

            return models.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}",
                ex.Message,
                nameof(WeatherRestService),
                nameof(WorkAsync));

            throw;
        }
        finally
        {
            _logger.LogInformation(
                "Finished | Service: {Service}, Method: {Method}, Time - {Datetime}",
                nameof(WeatherRestService),
                nameof(WorkAsync),
                DateTime.Now);
        }
    }

    #region WeatherExtensions Methods
    private async Task<List<WeatherModel>> ExistAsync(List<WeatherModel> weatherList)
    {
        try
        {
            var weatherParts = weatherList.Select(w => w.Part());
            return await GetAsync(weatherParts);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Model: {Model}",  
                ex.Message,
                nameof(WeatherRestService),
                nameof(ExistAsync),
                weatherList);
            
            throw;
        }
    }
    #endregion

    #region REST Methods
    private async Task<WeatherModel> InsertAsync(CreateWeatherModel createWeatherModel)
    {
        try
        {
            _logger.LogInformation(
                "Service: {Service}, Method: {Method}, Model: {Model}",    
                nameof(WeatherRestService),
                nameof(InsertAsync),
                createWeatherModel);
            
            const string relativeUri = $"weather";

            var resultModel =
                await _restRepository.Insert<WeatherModel, CreateWeatherModel>(new Uri(_api, relativeUri),
                    createWeatherModel);

            return resultModel;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Model: {Model}",  
                ex.Message,
                nameof(WeatherRestService),
                nameof(InsertAsync),
                createWeatherModel);
            
            throw;
        }
    }
    
    private async Task<List<WeatherModel>> GetAsync(IEnumerable<WeatherPart> weatherParts)
    {
        try
        {
            _logger.LogInformation(
                "Service: {Service}, Method: {Method}, Weathers: {Weathers}",   
                nameof(WeatherRestService),
                nameof(GetAsync),
                weatherParts);
            
            var resultList = new List<WeatherModel>();
            var partsList = await weatherParts.Sliced();
            
            foreach (var parts in partsList)
            {
                var responseModel = await _restRepository.Get<WeatherSummary>(new Uri(_api, Url(parts)));
                resultList.AddRange(responseModel.WeatherModels.ToList());
            }

            return resultList;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method},  Weathers: {Weathers}",    
                ex.Message,
                nameof(WeatherRestService),
                nameof(GetAsync),
                weatherParts);
            
            throw;
        }
    }
    
    private async Task<WeatherModel?> UpdateAsync(Guid weatherId, UpdateWeatherModel updateWeatherModel)
    {
        try
        {
            _logger.LogInformation(
                "Service: {Service}, Method: {Method}, Id: {Id}, Model: {Model}",   
                nameof(WeatherRestService),
                nameof(UpdateAsync),
                weatherId,
                updateWeatherModel);
            
            var relativeUri = $"weather/{weatherId}";

            var resultModel =
                await _restRepository.Update<WeatherModel, UpdateWeatherModel>(new Uri(_api, relativeUri),
                    updateWeatherModel);

            return resultModel;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Id: {Id}, Model: {Model}",      
                ex.Message,
                nameof(WeatherRestService),
                nameof(UpdateAsync),
                weatherId,
                updateWeatherModel);
            
            throw;
        }
    }
    #endregion

    #region Extensions
    private static string Url(WeatherPartsSummary parts)
    {
        var relativeUri = new StringBuilder();

        foreach (var section in parts.Sections)
        {
            var cityId = section.CityId;
            var forecastDateId = section.ForecastDateId;

            relativeUri.Append(string.IsNullOrEmpty(relativeUri.ToString())
                ? $"weather?CityIds={cityId}&ForecastDateIds={forecastDateId}"
                : $"&CityIds={cityId}&ForecastDateIds={forecastDateId}");
        }

        return relativeUri.ToString();
    }
    #endregion
}