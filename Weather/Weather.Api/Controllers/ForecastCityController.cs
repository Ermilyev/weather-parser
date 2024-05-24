using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using Common.Domain.ValueObjects;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Weather.Api.Models.Cities;
using Weather.Api.Services;

namespace Weather.Api.Controllers;

/// <summary>
/// Cities Controller
/// </summary>
/// <param name="service"></param>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Produces("application/json")]
public class ForecastCityController(ForecastCityService service) : Controller
{
    /// <summary>
    /// Get a list of cities
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <returns><see cref="PaginatedForecastCities"/></returns>
    [ProducesResponseType(typeof(PaginatedForecastCities), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<PaginatedForecastCities>> GetAllAsync(
        [FromQuery] ForecastCitiesFilter filter,
        [FromQuery] PaginationRecord pagination)
    {
        var ct = new CancellationToken();
        return Ok(await service.GetAllAsync(filter, pagination, ct));
    }
    
    /// <summary>
    /// Get a specific city
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <response code="404">City not found</response>
    /// <returns><see cref="ForecastCityModel"/></returns>
    [ProducesResponseType(typeof(ForecastCityModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ForecastCityModel>> GetAsync(
        [FromRoute, Required] Guid id)
    {
        var ct = new CancellationToken();
        var result = await service.GetAsync(id, ct);
        return result.ToActionResult();
    }
    
    /// <summary>
    /// Create a new city
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <returns><see cref="ForecastCityModel"/></returns>
    [ProducesResponseType(typeof(ForecastCityModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost]
    public async Task<ActionResult<ForecastCityModel>> CreateAsync(
        [FromBody] CreateForecastCityModel createModel)
    {
        var ct = new CancellationToken();
        var result = await service.CreateAsync(createModel, ct);
        return result.ToActionResult();
    }
    
    /// <summary>
    /// Update an existing city
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <response code="404">City not found</response>
    /// <response code="409">Conflict occurred while updating the city</response>
    /// <returns><see cref="ForecastCityModel"/></returns>
    [ProducesResponseType(typeof(ForecastCityModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ForecastCityModel>> UpdateAsync(
        [FromRoute, Required] Guid id,
        [FromBody] UpdateForecastCityModel updateModel)
    {
        var ct = new CancellationToken();
        var result = await service.UpdateAsync(id, updateModel, ct);
        return result.ToActionResult();
    }

    /// <summary>
    /// Delete a specific city
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <response code="404">City not found</response>
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