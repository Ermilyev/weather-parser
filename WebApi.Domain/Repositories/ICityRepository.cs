using Common.Domain.Repositories;
using Common.Models.Models;

namespace WebApi.Domain.Repositories;

public interface ICityRepository : ICRUDRepository<City>
{
    Task<IReadOnlyList<City>> GetByFilterAsync(Guid[]? ids = null, string[]? names = null);
    Task<City?> GetCityByNameAsync(string name);
}