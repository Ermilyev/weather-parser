using System.ComponentModel.DataAnnotations;
using Common.Domain.ValueObjects;

namespace Weather.Api.Models.Weathers;

public sealed class CreateForecastWeatherModel(EntityId cityId, EntityId dateId, string name)
{
    [Required] 
    public required EntityId CityId { get; init; } = cityId;
    [Required] 
    public required EntityId DateId { get; init; } = dateId;
    [Required] 
    public required string Name { get; init; } = name;
}