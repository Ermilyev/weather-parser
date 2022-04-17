using System.ComponentModel.DataAnnotations;
using System.Net;
using Common.Infrastructure.Utility.Serilog;
using Common.Models.ValueObject;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Weathers;
using WebApi.Models.Weathers.Aggregates;
using WebApi.Services.Services;

namespace WebApi.Services.Controllers.V1;

/// <inheritdoc />
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _service;

    /// <inheritdoc />
    public WeatherController(WeatherService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Создание погоды
    /// </summary>
    /// <param name="createModel"></param>
    /// <returns><see cref="WeatherModel"/></returns>
    /// <response code="400">Ошибка в параметрах.</response>
    /// <response code="409">Если уже существует с заданным городом и датой</response>
    [MapToApiVersion("1.0")]
    [HttpPost]
    [ProducesResponseType(typeof(WeatherModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [TrackUsage("WebApi", "WeatherController", "InsertAsync")]
    public async Task<ActionResult<WeatherModel>> InsertAsync(
        [FromBody] CreateWeatherModel createModel)
    {
        var result = await _service.InsertAsync(createModel);
        return result;
    }

    /// <summary>
    /// Получение списка доступной погоды
    /// </summary>
    /// <param name="weatherFilter"></param>
    /// <param name="pagination"></param>
    /// <returns><see cref="WeatherSummaryInfoModel"/></returns>
    [MapToApiVersion("1.0")]
    [HttpGet]
    [ProducesResponseType(typeof(WeatherSummaryInfoModel), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "WeatherController", "GetListAsync")]
    public async Task<ActionResult<WeatherSummaryInfoModel>> GetListAsync(
        [FromQuery] WeatherFilter weatherFilter, [Required, FromQuery] Pagination pagination)
    {
        var result  = await _service.GetListByFilterAsync(weatherFilter, pagination);
        return result;
    }
    
    /// <summary>
    /// Получение погоды по id
    /// </summary>
    /// <param name="weatherId"></param>
    /// <response code="404">Если погода не существует </response>
    [MapToApiVersion("1.0")]
    [HttpGet("{weatherId}")]
    [ProducesResponseType(typeof(WeatherModel), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "WeatherController", "GetAsync")]
    public async Task<ActionResult<WeatherModel>> GetAsync(
        [FromRoute][Required] Guid weatherId)
    {
        var result = await _service.GetAsync(weatherId);
        return result;
    }
    
    /// <summary>
    /// Обновление погоды
    /// </summary>
    /// <param name="weatherId"></param>
    /// <param name="updateModel"></param>
    /// <response code="400">Ошибка в параметрах.</response>
    /// <response code="404">Если погода не существует.</response>
    /// <response code="409">Если уже существует с заданным городом и датой</response>
    [MapToApiVersion("1.0")]
    [HttpPut("{weatherId}")]
    [ProducesResponseType(typeof(WeatherModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [TrackUsage("WebApi", "WeatherController", "UpdateAsync")]

    public async Task<ActionResult<WeatherModel>> UpdateAsync(
        [FromRoute][Required] Guid weatherId,
        [FromBody] UpdateWeatherModel updateModel)
    {
        var result = await _service.UpdateAsync(weatherId, updateModel);
        return result;
    }
    
    /// <summary>
    /// Удаление погоды
    /// </summary>
    /// <param name="weatherId"></param>
    /// <response code="404">Если погода не существует.</response>
    [MapToApiVersion("1.0")]
    [HttpDelete("{weatherId}")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "WeatherController", "DeleteAsync")]
    public async Task<ActionResult<Guid>> DeleteAsync(
        [FromRoute][Required] Guid weatherId)
    {
        var result = await _service.DeleteAsync(weatherId);
        return result;
    }
}