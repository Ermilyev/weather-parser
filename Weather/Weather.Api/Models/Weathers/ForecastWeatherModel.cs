using System.ComponentModel.DataAnnotations;
using Common.Domain.ValueObjects;

namespace Weather.Api.Models.Weathers;

public sealed class ForecastWeatherModel : EntityModel
{
    [Required] 
    public required EntityId CityId { get; init; }
    [Required] 
    public required EntityId DateId { get; init; }
    [Required] 
    public required string Name { get; init; }
}