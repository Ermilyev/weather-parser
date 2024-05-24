using Common.Domain.ValueObjects;

namespace Weather.Api.Models.Weathers;

public sealed class ForecastWeathersFilter : EntitiesFilter
{
    public HashSet<EntityId>? CityId { get; init; }
    public HashSet<EntityId>? DateId { get; init; }
}