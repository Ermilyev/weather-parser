using Common.Infrastructure.Repositories;
using Weather.Domain.Entities.Dates;
using Weather.Domain.Repositories.Dates;

namespace Weather.Infrastructure.Repositories.Dates;

public sealed class ForecastDateWriteRepository(MyDbContext dbContext)
    : WriteRepository<ForecastDate>(dbContext), IForecastDateWriteRepository
{
   
}