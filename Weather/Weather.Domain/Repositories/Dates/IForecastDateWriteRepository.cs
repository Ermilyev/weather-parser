using Common.Domain.Repositories;
using Weather.Domain.Entities.Dates;

namespace Weather.Domain.Repositories.Dates;

public interface IForecastDateWriteRepository : IWriteRepository<ForecastDate> { }