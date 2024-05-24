using Common.Domain.Repositories;
using Weather.Domain.Entities.Cities;

namespace Weather.Domain.Repositories.Cities;

public interface IForecastCityWriteRepository: IWriteRepository<ForecastCity> { }