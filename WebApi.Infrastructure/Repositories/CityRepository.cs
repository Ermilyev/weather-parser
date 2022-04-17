using Common.Infrastructure.Repositories;
using Common.Infrastructure.Utility.EF;
using Common.Models.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Repositories;

namespace WebApi.Infrastructure.Repositories;

public class CityRepository : CRUDRepository<City>, ICityRepository
{
    public  CityRepository(EFContext context) : base(context) { }

    public async Task<IReadOnlyList<City>> GetByFilterAsync(Guid[]? ids = null, string[]? names = null)
    {
        var entities = await GetAllAsync();

        if (ids is not null)
            entities = entities.Where(c => ids.Contains(c.Id)).ToList();

        if (names is not null)
            entities = entities.Where(c => names.Contains(c.Name.Trim())).ToList();

        return entities;
    }

    public async Task<City?> GetCityByNameAsync(string name)
    {
        return (await Context.Set<City>()
            .AsQueryable()
            .Where(x => x.Name == name)
            .FirstOrDefaultAsync());
    }
}