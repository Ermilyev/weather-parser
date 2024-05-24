using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using Common.Domain.ValueObjects;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Weather.Api.Models.Dates;
using Weather.Api.Services;

namespace Weather.Api.Controllers;

/// <summary>
/// Days Controller
/// </summary>
/// <param name="service"></param>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Produces("application/json")]
public class ForecastDateController(ForecastDateService service) : Controller
{
    /// <summary>
    /// Get a list of dates
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <returns><see cref="PaginatedForecastDates"/></returns>
    [ProducesResponseType(typeof(PaginatedForecastDates), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<PaginatedForecastDates>> GetAllAsync(
        [FromQuery] ForecastDatesFilter filter,
        [FromQuery] PaginationRecord pagination)
    {
        var ct = new CancellationToken();
        return Ok(await service.GetAllAsync(filter, pagination, ct));
    }
    
    /// <summary>
    /// Get a specific date
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <response code="404">Date not found</response>
    /// <returns><see cref="ForecastDateModel"/></returns>
    [ProducesResponseType(typeof(ForecastDateModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ForecastDateModel>> GetAsync(
        [FromRoute, Required] Guid id)
    {
        var ct = new CancellationToken();
        var result = await service.GetAsync(id, ct);
        return result.ToActionResult();
    }
    
    /// <summary>
    /// Create a new date
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <returns><see cref="ForecastDateModel"/></returns>
    [ProducesResponseType(typeof(ForecastDateModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost]
    public async Task<ActionResult<ForecastDateModel>> CreateAsync(
        [FromBody] CreateForecastDateModel createModel)
    {
        var ct = new CancellationToken();
        var result = await service.CreateAsync(createModel, ct);
        return result.ToActionResult();
    }
    
    /// <summary>
    /// Update an existing date
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <response code="404">Date not found</response>
    /// <response code="409">Conflict occurred while updating the date</response>
    /// <returns><see cref="ForecastDateModel"/></returns>
    [ProducesResponseType(typeof(ForecastDateModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ForecastDateModel>> UpdateAsync(
        [FromRoute, Required] Guid id, 
        [FromBody] UpdateForecastDateModel updateModel)
    {
        var ct = new CancellationToken();
        var result = await service.UpdateAsync(id, updateModel, ct);
        return result.ToActionResult();
    }
    
    /// <summary>
    /// Delete a specific date
    /// </summary>
    /// <response code="200">Request successful</response>
    /// <response code="400">Invalid request parameters</response>
    /// <response code="404">Date not found</response>
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