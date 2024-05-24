using Common.Infrastructure.Repositories;
using Weather.Domain.Entities.Cities;
using Weather.Domain.Repositories.Cities;

namespace Weather.Infrastructure.Repositories.Cities;

public sealed class ForecastCityWriteRepository(MyDbContext dbContext)
    : WriteRepository<ForecastCity>(dbContext), IForecastCityWriteRepository { }