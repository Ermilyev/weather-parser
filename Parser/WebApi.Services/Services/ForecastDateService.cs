using AutoMapper;
using Common.Models.Models;
using Common.Models.ValueObject;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Repositories;
using WebApi.Models.ForecastDates;
using WebApi.Models.ForecastDates.Aggregates;
using WebApi.Services.Validators.ForecastDates;

namespace WebApi.Services.Services;

/// <summary>
/// 
/// </summary>
public class ForecastDateService
{
    private readonly IMapper _mapper;
    private readonly ILogger<ForecastDateService> _logger;
    
    private readonly IForecastDateRepository _forecastDateRepository;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="forecastDateRepository"></param>
    public ForecastDateService(IMapper mapper, ILogger<ForecastDateService> logger,IForecastDateRepository forecastDateRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _forecastDateRepository = forecastDateRepository;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="forecastDateFilter"></param>
    /// <param name="pagination"></param>
    /// <returns></returns>
    public async Task<ActionResult<ForecastDateSummaryInfoModel>> GetListByFilterAsync(ForecastDateFilter forecastDateFilter,
        Pagination pagination)
    {
      
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Ids - {Ids}, Dates - {Dates}",  
                nameof(ForecastDateService),
                nameof(GetListByFilterAsync),
                forecastDateFilter.Ids,
                forecastDateFilter.Dates);
            
            var (ids, dates) = forecastDateFilter;
            var (skip, limit) = pagination;
            var entities = await _forecastDateRepository.GetByFilterAsync(ids?.ToArray(), dates?.ToArray());
            
            var count = entities.Count;
            var forecastDates = entities
                .OrderBy(forecastDate => forecastDate.Date.Date)
                .Skip(skip)
                .Take(limit ?? 100)
                .Select(forecastDate => _mapper.Map<ForecastDateModel>(forecastDate));
            
            return new ForecastDateSummaryInfoModel(){ForecastDateModels = forecastDates.ToArray(), Count = count};
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: Ids - {Ids}, Dates - {Dates}", 
                ex.Message,
                nameof(ForecastDateService),
                nameof(GetListByFilterAsync), 
                forecastDateFilter.Ids,
                forecastDateFilter.Dates);
            
            throw;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="createModel"></param>
    /// <returns></returns>
    public async Task<ActionResult<ForecastDateModel>> InsertAsync(CreateForecastDateModel createModel)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Date - {Date}",  
                nameof(ForecastDateService),
                nameof(InsertAsync), 
                createModel.Date);

            var validator = new CreateForecastDateValidator();
            var validatorResult = validator.Validate(createModel);
            if (validatorResult.IsValid == false)
                return new BadRequestResult();

            if (await IsExist(createModel.Date))
                return new ConflictResult();

            var forecastDate = new ForecastDate()
            {
                Id = Guid.NewGuid(),
                Date = createModel.Date
            };

            await _forecastDateRepository.InsertAsync(forecastDate);

            return  _mapper.Map<ForecastDateModel>(await _forecastDateRepository.GetOneAsync(forecastDate.Id));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: Date - {Date}",  
                ex.Message,
                nameof(ForecastDateService),
                nameof(InsertAsync), 
                createModel.Date);
            
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="forecastDateId"></param>
    /// <returns></returns>
    public async Task<ActionResult<ForecastDateModel>>  GetAsync(Guid forecastDateId)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}",  
                nameof(ForecastDateService),
                nameof(GetAsync), 
                forecastDateId);
            
            var forecastDate = await _forecastDateRepository.GetOneAsync(forecastDateId);
            return forecastDate is null ? new NotFoundResult() : _mapper.Map<ForecastDateModel>(forecastDate);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: Id - {Id}",  
                ex.Message,
                nameof(ForecastDateService),
                nameof(GetAsync), 
                forecastDateId);
            
            throw;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="forecastDateId"></param>
    /// <param name="updateModel"></param>
    /// <returns></returns>
    public async Task<ActionResult<ForecastDateModel>> UpdateAsync(Guid forecastDateId, UpdateForecastDateModel updateModel)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}, Date - {Date}",  
                nameof(ForecastDateService),
                nameof(UpdateAsync), 
                forecastDateId,
                updateModel.Date);
            
            var forecastDate = await _forecastDateRepository.GetOneAsync(forecastDateId);
        
            if (forecastDate is null)
                return new NotFoundResult();

            var validator = new UpdateForecastDateValidator();
            var validatorResult = validator.Validate(updateModel);
            if (validatorResult.IsValid == false)
                return new BadRequestResult();

            if (await IsExist(updateModel.Date))
                return new ConflictResult();

            if (forecastDate.Date.Equals(updateModel.Date) == false)
            {
                forecastDate.Date = updateModel.Date;
                await _forecastDateRepository.UpdateAsync(forecastDate);
            }
        
            return _mapper.Map<ForecastDateModel>(await _forecastDateRepository.GetOneAsync(forecastDate.Id));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: Id - {Id}, Date - {Date}",  
                ex.Message,
                nameof(ForecastDateService),
                nameof(UpdateAsync), 
                forecastDateId,
                updateModel.Date);
            
            throw;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="forecastDateId"></param>
    /// <returns></returns>
    public async Task<ActionResult<Guid>> DeleteAsync(Guid forecastDateId)
    {
        try
        {
            _logger.LogInformation("Service: {Service}, Method: {Method}, Properties: Id - {Id}",  
                nameof(ForecastDateService),
                nameof(DeleteAsync), 
                forecastDateId);
            
            var forecastDate = await _forecastDateRepository.GetOneAsync(forecastDateId);
        
            if (forecastDate is null)
                return new NotFoundResult();

            await _forecastDateRepository.RemoveAsync(forecastDate);
        
            return forecastDateId;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: Id - {Id}",  
                ex.Message,
                nameof(ForecastDateService),
                nameof(DeleteAsync), 
                forecastDateId);
            
            throw;
        }
    }
    
    private async Task<bool> IsExist(DateTime modelDate)
    {
        try
        {
            return await _forecastDateRepository.GetForecastDateByDateAsync(modelDate) is not null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Error}, Service: {Service}, Method: {Method}, Properties: ModelDate - {Model}",  
                ex.Message,
                nameof(ForecastDateService),
                nameof(IsExist), 
                modelDate);
            
            throw;
        }
    }
}