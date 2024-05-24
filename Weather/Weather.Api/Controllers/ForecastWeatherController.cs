using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using Common.Domain.ValueObjects;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Weather.Api.Models.Weathers;
using Weather.Api.Services;

namespace Weather.Api.Controllers;

/// <summary>
/// Weathers Controller
/// </summary>
/// <param name="service"></param>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Produces("application/json")]
public class ForecastWeatherController(ForecastWeatherService service) : Controller
{
    /// <summary>
    /// Gets a list of all weather forecasts.
    /// </summary>
    /// <param name="filter">The filter criteria for the forecasts.</param>
    /// <param name="pagination">The pagination details.</param>
    /// <returns>A list of weather forecasts.</returns>
    /// <response code="200">Successfully retrieved the list of forecasts.</response>
    /// <response code="400">Invalid request parameters.</response>
    /// <returns><see cref="PaginatedForecastWeathers"/></returns>
    [ProducesResponseType(typeof(PaginatedForecastWeathers), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<PaginatedForecastWeathers>> GetAllAsync(
        [FromQuery] ForecastWeathersFilter filter,
        [FromQuery] PaginationRecord pagination)
    {
        var ct = new CancellationToken();
        return Ok(await service.GetAllAsync(filter, pagination, ct));
    }
    
    /// <summary>
    /// Gets a specific weather forecast by ID.
    /// </summary>
    /// <param name="id">The ID of the forecast to retrieve.</param>
    /// <returns>The requested weather forecast.</returns>
    /// <response code="200">Successfully retrieved the forecast.</response>
    /// <response code="400">Invalid request parameters.</response>
    /// <response code="404">The forecast was not found.</response>
    /// <returns><see cref="ForecastWeatherModel"/></returns>
    [ProducesResponseType(typeof(ForecastWeatherModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ForecastWeatherModel>> GetAsync(
        [FromRoute, Required] Guid id)
    {
        var ct = new CancellationToken();
        var result = await service.GetAsync(id, ct);
        return result.ToActionResult();
    }
    
    /// <summary>
    /// Creates a new weather forecast.
    /// </summary>
    /// <param name="createModel">The details of the forecast to create.</param>
    /// <returns>The created weather forecast.</returns>
    /// <response code="200">Successfully created the forecast.</response>
    /// <response code="400">Invalid request parameters.</response>
    /// <response code="409">A forecast with the same details already exists.</response>
    /// <returns><see cref="ForecastWeatherModel"/></returns>
    [ProducesResponseType(typeof(ForecastWeatherModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost]
    public async Task<ActionResult<ForecastWeatherModel>> CreateAsync(
        [FromBody] CreateForecastWeatherModel createModel)
    {
        var ct = new CancellationToken();
        var result = await service.CreateAsync(createModel, ct);
        return result.ToActionResult();
    }
    
    /// <summary>
    /// Updates an existing weather forecast.
    /// </summary>
    /// <param name="id">The ID of the forecast to update.</param>
    /// <param name="updateModel">The updated forecast details.</param>
    /// <returns>The updated weather forecast.</returns>
    /// <response code="200">Successfully updated the forecast.</response>
    /// <response code="400">Invalid request parameters.</response>
    /// <response code="404">The forecast was not found.</response>
    /// <response code="409">A conflict occurred while updating the forecast.</response>
    /// <returns><see cref="UpdateForecastWeatherModel"/></returns>
    [ProducesResponseType(typeof(ForecastWeatherModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ForecastWeatherModel>> UpWeatherAsync(
        [FromRoute, Required] Guid id, 
        [FromBody] UpdateForecastWeatherModel updateModel)
    {
        var ct = new CancellationToken();
        var result = await service.UpdateAsync(id, updateModel, ct);
        return result.ToActionResult();
    }
    
    /// <summary>
    /// Deletes a specific weather forecast by ID.
    /// </summary>
    /// <param name="id">The ID of the forecast to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="200">Successfully deleted the forecast.</response>
    /// <response code="400">Invalid request parameters.</response>
    /// <response code="404">The forecast was not found.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync([FromRoute, Required] Guid id)
    {
        var ct = new CancellationToken();
        var result = await service.DeleteAsync(id, ct);
        return result.ToActionResult();
    }
}