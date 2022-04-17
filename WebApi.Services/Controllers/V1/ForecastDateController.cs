using System.ComponentModel.DataAnnotations;
using System.Net;
using Common.Infrastructure.Utility.Serilog;
using Common.Models.ValueObject;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.ForecastDates;
using WebApi.Models.ForecastDates.Aggregates;
using WebApi.Services.Services;

namespace WebApi.Services.Controllers.V1;

/// <summary>
/// 
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ForecastDateController
{
    private readonly ForecastDateService _service;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
    public ForecastDateController(ForecastDateService service) 
        => _service = service;

    /// <summary>
    /// Создание даты
    /// </summary>
    /// <param name="createModel"></param>
    /// <returns><see cref="ForecastDateModel"/></returns>
    /// <response code="400">Ошибка в параметрах.</response>
    /// <response code="409">Если уже существует с заданной датой</response>
    [MapToApiVersion("1.0")]
    [HttpPost]
    [ProducesResponseType(typeof(ForecastDateModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [TrackUsage("WebApi", "ForecastDateController", "InsertAsync")]
    public async Task<ActionResult<ForecastDateModel>> InsertAsync(
        [FromBody] CreateForecastDateModel createModel)
    {
        var result = await _service.InsertAsync(createModel);
        return result;
    }
    
    /// <summary>
    /// Получение списка доступных дат
    /// </summary>
    /// <param name="forecastDateFilter"></param>
    /// <param name="pagination"></param>
    /// <returns><see cref="ForecastDateSummaryInfoModel"/></returns>
    [MapToApiVersion("1.0")]
    [HttpGet]
    [ProducesResponseType(typeof(ForecastDateSummaryInfoModel), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "ForecastDateController", "GetListAsync")]
    public async Task<ActionResult<ForecastDateSummaryInfoModel>> GetListAsync(
        [FromQuery] ForecastDateFilter forecastDateFilter, [Required, FromQuery] Pagination pagination)
    {
        var result  = await _service.GetListByFilterAsync(forecastDateFilter, pagination);
        return result;
    }
    
    /// <summary>
    /// Получение даты по id
    /// </summary>
    /// <param name="dateId"></param>
    /// <response code="404">Если дата не существует </response>
    [MapToApiVersion("1.0")]
    [HttpGet("{dateId}")]
    [ProducesResponseType(typeof(ForecastDateModel), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "ForecastDateController", "GetAsync")]
    public async Task<ActionResult<ForecastDateModel>> GetAsync(
        [FromRoute][Required] Guid dateId)
    {
        var result = await _service.GetAsync(dateId);
        return result;
    }
    
    /// <summary>
    /// Обновление даты
    /// </summary>
    /// <param name="dateId"></param>
    /// <param name="updateModel"></param>
    /// <response code="400">Ошибка в параметрах.</response>
    /// <response code="404">Если дата не существует.</response>
    /// <response code="409">Если уже существует с заданной датой</response>
    [MapToApiVersion("1.0")]
    [HttpPut("{dateId}")]
    [ProducesResponseType(typeof(ForecastDateModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [TrackUsage("WebApi", "ForecastDateController", "UpdateAsync")]
    public async Task<ActionResult<ForecastDateModel>> UpdateAsync(
        [FromRoute][Required] Guid dateId,
        [FromBody] UpdateForecastDateModel updateModel)
    {
        var result = await _service.UpdateAsync(dateId, updateModel);
        return result;
    }
    
    /// <summary>
    /// Удаление даты
    /// </summary>
    /// <param name="dateId"></param>
    /// <response code="404">Если дата не существует.</response>
    [MapToApiVersion("1.0")]
    [HttpDelete("{dateId}")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "ForecastDateController", "DeleteAsync")]
    public async Task<ActionResult<Guid>> DeleteAsync(
        [FromRoute][Required] Guid dateId)
    {
        var result = await _service.DeleteAsync(dateId);
        return result;
    }
}