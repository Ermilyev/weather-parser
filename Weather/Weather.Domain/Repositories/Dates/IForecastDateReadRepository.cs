using Common.Domain.Repositories;
using Weather.Domain.Entities.Dates;

namespace Weather.Domain.Repositories.Dates;

public interface IForecastDateReadRepository : IReadRepository<ForecastDate>
{
    Task<bool> ExistsWithDayAsync(DateTime day, CancellationToken ct);
}