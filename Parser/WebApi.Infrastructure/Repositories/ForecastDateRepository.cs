using Common.Infrastructure.Repositories;
using Common.Infrastructure.Utility.EF;
using Common.Models.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Repositories;

namespace WebApi.Infrastructure.Repositories;

public class ForecastDateRepository : CRUDRepository<ForecastDate>, IForecastDateRepository
{
    public ForecastDateRepository(EFContext context) : base(context) { }
    
    public async Task<IReadOnlyList<ForecastDate>> GetByFilterAsync(Guid[]? ids = null, DateTime[]? dates = null)
    {
        var entities = await GetAllAsync();

        if (ids is not null)
            entities = entities.Where(c => ids.Contains(c.Id)).ToList();
        
        if (dates is not null)
            entities = entities.Where(c => dates.Contains(c.Date)).ToList();

        return entities;
    }

    public async Task<ForecastDate?> GetForecastDateByDateAsync(DateTime date)
    {
        return (await Context.Set<ForecastDate>()
            .AsQueryable()
            .Where(x => x.Date == date)
            .FirstOrDefaultAsync());
    }
}