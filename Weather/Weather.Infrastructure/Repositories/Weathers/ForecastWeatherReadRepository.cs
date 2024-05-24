using Common.Domain.ValueObjects;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities.Weathers;
using Weather.Domain.Repositories.Weathers;

namespace Weather.Infrastructure.Repositories.Weathers;

public sealed class ForecastWeatherReadRepository(MyDbContext dbContext)
    : ReadRepository<ForecastWeather>(dbContext), IForecastWeatherReadRepository
{
    public async Task<bool> ExistsWithParamsAsync(EntityId cityId, EntityId dateId, CancellationToken ct)
    {
        return await dbContext.Set<ForecastWeather>()
            .AnyAsync(e
                => EF.Property<EntityId>(e, "CityId") == cityId && 
                   EF.Property<EntityId>(e, "DateId") == dateId, 
                cancellationToken: ct);
    }
}
