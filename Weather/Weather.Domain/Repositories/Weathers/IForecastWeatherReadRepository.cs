using Common.Domain.Repositories;
using Common.Domain.ValueObjects;
using Weather.Domain.Entities.Weathers;

namespace Weather.Domain.Repositories.Weathers;

public interface IForecastWeatherReadRepository : IReadRepository<ForecastWeather>
{
    Task<bool> ExistsWithParamsAsync(EntityId cityId, EntityId dateId, CancellationToken ct);
}