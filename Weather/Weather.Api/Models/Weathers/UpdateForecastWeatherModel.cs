using Common.Domain.ValueObjects;

namespace Weather.Api.Models.Weathers;

public sealed class UpdateForecastWeatherModel(EntityId? cityId, EntityId? dateId, string? name)
{
    public EntityId? CityId { get; init; } = cityId;
    public required EntityId? DateId { get; init; } = dateId;
    public required string? Name { get; init; } = name;
}
