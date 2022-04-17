using Common.Domain.Repositories;
using Common.Models.Models;

namespace WebApi.Domain.Repositories;

public interface IForecastDateRepository : ICRUDRepository<ForecastDate>
{
    Task<IReadOnlyList<ForecastDate>> GetByFilterAsync(Guid[]? ids = null, DateTime[]? dates = null);
    Task<ForecastDate?> GetForecastDateByDateAsync(DateTime date);
}