using Common.Domain.Repositories;
using Weather.Domain.Entities.Cities;

namespace Weather.Domain.Repositories.Cities;

public interface IForecastCityReadRepository : IReadRepository<ForecastCity>
{
    Task<bool> ExistsWithNameAsync(string name, CancellationToken ct);
}