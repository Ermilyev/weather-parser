using Common.Domain.Repositories;
using Weather.Domain.Entities.Weathers;

namespace Weather.Domain.Repositories.Weathers;

public interface IForecastWeatherWriteRepository: IWriteRepository<ForecastWeather> { }