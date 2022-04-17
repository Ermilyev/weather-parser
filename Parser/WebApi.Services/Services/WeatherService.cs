using AutoMapper;
using Common.Models.Models;
using Common.Models.ValueObject;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Repositories;
using WebApi.Models.Weathers;
using WebApi.Models.Weathers.Aggregates;
using WebApi.Services.Validators.Weathers;

namespace WebApi.Services.Services;

/// <summary>
/// 
/// </summary>
public class WeatherService
{
    private readonly IMapper _mapper;
    private readonly ILogger<WeatherService> _logger;

    private readonly IWeatherRepository _weatherRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="weatherRepository"></param>
    public WeatherService(IMapper mapper, ILogger<WeatherService> logger, IWeatherRepository weatherRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _weatherRepository = weatherRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weatherFilter"></param>
    /// <param name="pagination"></param>
    /// <returns></returns>
    public async Task<ActionResult<WeatherSummaryInfoModel>> GetListByFilterAsync(WeatherFilter weatherFilter,
        Pagination pagination)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Ids - {Ids}, " +
                                "CityIds - {Cities}, DateIds - {Dates}",
                nameof(WeatherService),
                nameof(GetListByFilterAsync),
                weatherFilter.Ids,
                weatherFilter.CityIds,
                weatherFilter.ForecastDateIds);

            var (ids, cityIds, forecastDateIds) = weatherFilter;
            var (skip, limit) = pagination;
            var entities = await _weatherRepository.GetByFilterAsync(ids?.ToArray(), cityIds?.ToArray(), forecastDateIds?.ToArray());

            var count = entities.Count;
            var weathers = entities
                .Skip(skip)
                .Take(limit ?? 25)
                .Select(weather => _mapper.Map<WeatherModel>(weather));

            return new WeatherSummaryInfoModel() { WeatherModels = weathers.ToArray(), Count = count };
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Properties: Ids - {Ids}, " +
                "CityIds - {Cities}, DateIds - {Dates}",
                ex.Message,
                nameof(WeatherService),
                    nameof(GetListByFilterAsync),
                    weatherFilter.Ids,
                    weatherFilter.CityIds,
                    weatherFilter.ForecastDateIds);

            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createModel"></param>
    /// <returns></returns>
    public async Task<ActionResult<WeatherModel>> InsertAsync(CreateWeatherModel createModel)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Model - {Model} ",
                nameof(WeatherService),
                nameof(InsertAsync),
                createModel);

            var validator = new CreateWeatherValidator();
            var validatorResult = validator.Validate(createModel);
            if (validatorResult.IsValid == false)
                return new BadRequestResult();

            if (await IsExist(createModel.CityId, createModel.ForecastDateId))
                return new ConflictResult();

            var weather = new Weather()
            {
                Id = Guid.NewGuid(),
                CityId = createModel.CityId,
                ForecastDateId = createModel.ForecastDateId,
                ParsedAt = createModel.ParsedAt,
                MinTempCelsius = createModel.MinTempCelsius,
                MinTempFahrenheit = createModel.MinTempFahrenheit,
                MaxTempCelsius = createModel.MaxTempCelsius,
                MaxTempFahrenheit = createModel.MaxTempFahrenheit,
                MaxWindSpeedMetersPerSecond = createModel.MaxWindSpeedMetersPerSecond,
                MaxWindSpeedMilesPerHour = createModel.MaxWindSpeedMilesPerHour
            };

            await _weatherRepository.InsertAsync(weather);

            return _mapper.Map<WeatherModel>(await _weatherRepository.GetOneAsync(weather.Id));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Properties: Model - {Model} ",
                ex.Message,
                nameof(WeatherService),
                nameof(InsertAsync),
                createModel);

            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weatherId"></param>
    /// <returns></returns>
    public async Task<ActionResult<WeatherModel>> GetAsync(Guid weatherId)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}",
                nameof(WeatherService),
                nameof(GetAsync),
                weatherId);

            var weather = await _weatherRepository.GetOneAsync(weatherId);
            return weather is null ? new NotFoundResult() : _mapper.Map<WeatherModel>(weather);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Properties: Id - {Id}",
                ex.Message,
                nameof(WeatherService),
                nameof(GetAsync),
                weatherId);

            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weatherId"></param>
    /// <param name="updateModel"></param>
    /// <returns></returns>
    public async Task<ActionResult<WeatherModel>> UpdateAsync(Guid weatherId, UpdateWeatherModel updateModel)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}, " +
                                   "Model - {Model}",
                nameof(WeatherService),
                nameof(UpdateAsync),
                weatherId,
                updateModel);

            var weather = await _weatherRepository.GetOneAsync(weatherId);

            if (weather is null)
                return new NotFoundResult();

            var validator = new UpdateWeatherValidator();
            var validatorResult = validator.Validate(updateModel);
            if (validatorResult.IsValid == false)
                return new BadRequestResult();

            if (await IsExist(updateModel.CityId, updateModel.ForecastDateId))
                return new ConflictResult();

            weather.CityId = updateModel.CityId;
            weather.ForecastDateId = updateModel.ForecastDateId;
            weather.ParsedAt = updateModel.ParsedAt;
            weather.MinTempCelsius = updateModel.MinTempCelsius;
            weather.MinTempFahrenheit = updateModel.MinTempFahrenheit;
            weather.MaxTempCelsius = updateModel.MaxTempCelsius;
            weather.MaxTempFahrenheit = updateModel.MaxTempFahrenheit;
            weather.MaxWindSpeedMetersPerSecond = updateModel.MaxWindSpeedMetersPerSecond;
            weather.MaxWindSpeedMilesPerHour = updateModel.MaxWindSpeedMilesPerHour;

            await _weatherRepository.UpdateAsync(weather);
            return _mapper.Map<WeatherModel>(await _weatherRepository.GetOneAsync(weather.Id));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Properties: Ids - {Ids}, " +
                "CityIds - {Cities}, DateIds - {Dates}",
                ex.Message,
                nameof(WeatherService),
                nameof(UpdateAsync),
                weatherId,
                updateModel.CityId,
                updateModel.ForecastDateId);

            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weatherId"></param>
    /// <returns></returns>
    public async Task<ActionResult<Guid>> DeleteAsync(Guid weatherId)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}",
                nameof(WeatherService),
                nameof(DeleteAsync),
                weatherId);

            var weather = await _weatherRepository.GetOneAsync(weatherId);

            if (weather is null)
                return new NotFoundResult();

            await _weatherRepository.RemoveAsync(weather);

            return weatherId;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Properties: Id - {Id}",
                ex.Message,
                nameof(WeatherService),
                nameof(DeleteAsync),
                weatherId);

            throw;
        }
    }

    private async Task<bool> IsExist(Guid cityId, Guid forecastDateId)
    {
        try
        {
            return await _weatherRepository.GetWeatherAsync(cityId, forecastDateId) is not null;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Properties: CityId - {City}, DateId - {Date}",
                ex.Message,
                nameof(WeatherService),
                nameof(IsExist),
                cityId,
                forecastDateId);

            throw;
        }
    }
}