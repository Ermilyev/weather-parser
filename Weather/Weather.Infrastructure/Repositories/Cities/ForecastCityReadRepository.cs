using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities.Cities;
using Weather.Domain.Repositories.Cities;

namespace Weather.Infrastructure.Repositories.Cities;

public sealed class ForecastCityReadRepository(MyDbContext dbContext)
    : ReadRepository<ForecastCity>(dbContext), IForecastCityReadRepository
{
    public async Task<bool> ExistsWithNameAsync(string name, CancellationToken ct)
    {
        return await dbContext.
            Set<ForecastCity>()
            .AnyAsync(e
                => EF.Property<string>(e, "Name") == name, cancellationToken: ct);
    }
}