using Common.Infrastructure.Repositories;
using Weather.Domain.Entities.Weathers;
using Weather.Domain.Repositories.Weathers;

namespace Weather.Infrastructure.Repositories.Weathers;

public sealed class ForecastWeatherWriteRepository(MyDbContext dbContext)
    : WriteRepository<ForecastWeather>(dbContext), IForecastWeatherWriteRepository { }