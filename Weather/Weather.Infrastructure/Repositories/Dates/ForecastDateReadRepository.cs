using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities.Dates;
using Weather.Domain.Repositories.Dates;

namespace Weather.Infrastructure.Repositories.Dates;

public sealed class ForecastDateReadRepository(MyDbContext dbContext)
    : ReadRepository<ForecastDate>(dbContext), IForecastDateReadRepository
{
    public async Task<bool> ExistsWithDayAsync(DateTime day, CancellationToken ct)
    {
        return await dbContext.Set<ForecastDate>()
            .AnyAsync(e
                => EF.Property<DateTime>(e, "Day") == day, cancellationToken: ct);
    }
}