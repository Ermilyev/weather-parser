using System.ComponentModel.DataAnnotations;
using System.Net;
using Common.Infrastructure.Utility.Serilog;
using Common.Models.ValueObject;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Cities;
using WebApi.Models.Cities.Aggregates;
using WebApi.Services.Services;

namespace WebApi.Services.Controllers.V1;

/// <inheritdoc />
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CityController : ControllerBase
{
    private readonly CityService _service;

    /// <inheritdoc />
    public CityController(CityService service)
        => _service = service;

    /// <summary>
    /// Создание города
    /// </summary>
    /// <param name="createModel"></param>
    /// <returns><see cref="CityModel"/></returns>
    /// <response code="400">Ошибка в параметрах.</response>
    /// <response code="409">Если уже существует с заданным именем</response>
    [MapToApiVersion("1.0")]
    [HttpPost]
    [ProducesResponseType(typeof(CityModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [TrackUsage("WebApi", "CityController", "InsertAsync")]
    public async Task<ActionResult<CityModel>> InsertAsync(
        [FromBody] CreateCityModel createModel)
    {
        var result = await _service.InsertAsync(createModel);
        return result;
    }

    /// <summary>
    /// Получение списка доступных городов
    /// </summary>
    /// <param name="cityFilter"></param>
    /// <param name="pagination"></param>
    /// <returns><see cref="CitySummaryInfoModel"/></returns>
    [MapToApiVersion("1.0")]
    [HttpGet] 
    [ProducesResponseType(typeof(CitySummaryInfoModel), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "CityController", "GetListAsync")]
    public async Task<ActionResult<CitySummaryInfoModel>> GetListAsync(
        [FromQuery] CityFilter cityFilter, [Required, FromQuery] Pagination pagination)
    {
        var result  = await _service.GetListByFilterAsync(cityFilter, pagination);
        return result;
    }
    
    /// <summary>
    /// Получение города по id
    /// </summary>
    /// <param name="cityId"></param>
    /// <response code="404">Если город не существует </response>
    [MapToApiVersion("1.0")]
    [HttpGet("{cityId}")]
    [ProducesResponseType(typeof(CityModel), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "CityController", "GetAsync")]
    public async Task<ActionResult<CityModel>> GetAsync(
        [FromRoute][Required] Guid cityId)
    {
        var result = await _service.GetAsync(cityId);
        return result;
    }
    
    /// <summary>
    /// Обновление города
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="updateModel"></param>
    /// <response code="400">Ошибка в параметрах.</response>
    /// <response code="404">Если город не существует.</response>
    /// <response code="409">Если уже существует с заданным именем</response>
    [MapToApiVersion("1.0")]
    [HttpPut("{cityId}")]
    [ProducesResponseType(typeof(CityModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [TrackUsage("WebApi", "CityController", "UpdateAsync")]
    public async Task<ActionResult<CityModel>> UpdateAsync(
        [FromRoute][Required] Guid cityId,
        [FromBody] UpdateCityModel updateModel)
    {
        var result = await _service.UpdateAsync(cityId, updateModel);
        return result;
    }
    
    /// <summary>
    /// Удаление города
    /// </summary>
    /// <param name="cityId"></param>
    /// <response code="404">Если город не существует.</response>
    [MapToApiVersion("1.0")]
    [HttpDelete("{cityId}")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [TrackUsage("WebApi", "CityController", "DeleteAsync")]
    public async Task<ActionResult<Guid>> DeleteAsync(
        [FromRoute][Required] Guid cityId)
    {
        var result = await _service.DeleteAsync(cityId);
        return result;
    }
}