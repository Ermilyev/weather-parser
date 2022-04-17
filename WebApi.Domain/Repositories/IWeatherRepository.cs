using Common.Domain.Repositories;
using Common.Models.Models;

namespace WebApi.Domain.Repositories;

public interface IWeatherRepository: ICRUDRepository<Weather>
{
    Task<IReadOnlyList<Weather>> GetByFilterAsync(Guid[]? ids = null, Guid[]? cityIds = null, Guid[]? forecastDateIds = null);
    Task<Weather?> GetWeatherAsync(Guid cityId, Guid forecastDateId);
}