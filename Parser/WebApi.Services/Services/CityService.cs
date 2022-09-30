using AutoMapper;
using Common.Models.Models;
using Common.Models.ValueObject;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Repositories;
using WebApi.Models.Cities;
using WebApi.Models.Cities.Aggregates;
using WebApi.Services.Validators.Cities;

namespace WebApi.Services.Services;

/// <summary>
/// 
/// </summary>
public class CityService
{
    private readonly IMapper _mapper;
    private readonly ILogger<CityService> _logger;
    
    private readonly ICityRepository _cityRepository;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="cityRepository"></param>
    public CityService(IMapper mapper, ILogger<CityService> logger, ICityRepository cityRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _cityRepository = cityRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cityFilter"></param>
    /// <param name="pagination"></param>
    /// <returns></returns>
    public async Task<ActionResult<CitySummaryInfoModel>> GetListByFilterAsync(CityFilter cityFilter,
        Pagination pagination)
    {
        _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Ids - {Ids}, Names - {Names}",
            nameof(CityService),
            nameof(GetListByFilterAsync),
            cityFilter.Ids,
            cityFilter.Names);

        var (ids, names) = cityFilter;
        var (skip, limit) = pagination;
        var entities = await _cityRepository.GetByFilterAsync(ids?.ToArray(), names?.ToArray());

        var count = entities.Count;
        var cities = entities
            .OrderBy(city => city.Name)
            .Skip(skip)
            .Take(limit ?? 100)
            .Select(city => _mapper.Map<CityModel>(city));

        return new CitySummaryInfoModel() {CityModelList = cities.ToArray(), Count = count};
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createModel"></param>
    /// <returns></returns>
    public async Task<ActionResult<CityModel>> InsertAsync(CreateCityModel createModel)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Name - {Name}",  
                nameof(CityService),
                nameof(InsertAsync), 
                createModel.Name);

            var validator = new CreateCityValidator();
            var validatorResult = validator.Validate(createModel);
            if (validatorResult.IsValid == false)
                return new BadRequestResult();

            if (await IsExist(createModel.Name))
                return new ConflictResult();

            var city = new City()
            {
                Id = Guid.NewGuid(),
                Name = createModel.Name.Trim()
            };

            await _cityRepository.InsertAsync(city);

            return _mapper.Map<CityModel>(await _cityRepository.GetOneAsync(city.Id));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: Name - {Name}",  
                ex.Message,
                nameof(CityService),
                nameof(InsertAsync), 
                createModel.Name);
            
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cityId"></param>
    /// <returns></returns>
    public async Task<ActionResult<CityModel>>  GetAsync(Guid cityId)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}",  
                nameof(CityService),
                nameof(GetAsync), 
                cityId);
            
            var city = await _cityRepository.GetOneAsync(cityId);
            return city is null ? new NotFoundResult() : _mapper.Map<CityModel>(city);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: Id - {Id}",  
                ex.Message,
                nameof(CityService),
                nameof(GetAsync), 
                cityId);
            
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="updateModel"></param>
    /// <returns></returns>
    public async Task<ActionResult<CityModel>> UpdateAsync(Guid cityId, UpdateCityModel updateModel)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}, Name - {Name}",
                nameof(CityService),
                nameof(UpdateAsync),
                cityId,
                updateModel.Name);

            var city = await _cityRepository.GetOneAsync(cityId);

            if (city is null)
                return new NotFoundResult();

            var validator = new UpdateCityValidator();
            var validatorResult = validator.Validate(updateModel);
            if (validatorResult.IsValid == false)
                return new BadRequestResult();

            if (await IsExist(updateModel.Name))
                return new ConflictResult();

            if (city.Name.Trim().ToLower().Equals(updateModel.Name.Trim().ToLower()) == false)
            {
                city.Name = updateModel.Name;
                await _cityRepository.UpdateAsync(city);
            }

            return _mapper.Map<CityModel>(await _cityRepository.GetOneAsync(city.Id));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Properties: Id - {Id}, Name - {Name}",
                ex.Message,
                nameof(CityService),
                nameof(UpdateAsync),
                cityId,
                updateModel.Name);

            throw;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cityId"></param>
    /// <returns></returns>
    public async Task<ActionResult<Guid>> DeleteAsync(Guid cityId)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}",  
                nameof(CityService),
                nameof(DeleteAsync), 
                cityId);
            
            var city = await _cityRepository.GetOneAsync(cityId);
        
            if (city is null)
                return new NotFoundResult();

            await _cityRepository.RemoveAsync(city);

            return cityId;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: Id - {Id}",  
                ex.Message,
                nameof(CityService),
                nameof(DeleteAsync), 
                cityId);
            
            throw;
        }
    }

    private async Task<bool> IsExist(string modelName)
    {
        try
        {
            return await _cityRepository.GetCityByNameAsync(modelName.ToLower().Trim()) is not null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: ModelName - {Model}",  
                ex.Message,
                nameof(CityService),
                nameof(IsExist), 
                modelName);
            
            throw;
        }
    }
}