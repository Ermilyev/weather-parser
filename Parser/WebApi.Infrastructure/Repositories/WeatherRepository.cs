using Common.Infrastructure.Repositories;
using Common.Infrastructure.Utility.EF;
using Common.Models.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Repositories;

namespace WebApi.Infrastructure.Repositories;

public class WeatherRepository : CRUDRepository<Weather>, IWeatherRepository
{
    public WeatherRepository(EFContext context) : base(context) {}

    public async Task<IReadOnlyList<Weather>> GetByFilterAsync(Guid[]? ids = null, Guid[]? cityIds = null,
        Guid[]? forecastDateIds = null)
    {
        var entities = await GetAllAsync();

        if (ids is not null)
            entities = entities.Where(item => ids.Contains(item.Id)).ToList();

        if (cityIds is not null)
            entities = entities.Where(item => cityIds.Contains(item.CityId)).ToList();

        if (forecastDateIds is not null)
            entities = entities.Where(item => forecastDateIds.Contains(item.ForecastDateId)).ToList();

        return entities.ToArray();
    }

    public async Task<Weather?> GetWeatherAsync(Guid cityId, Guid forecastDateId)
    {
        return (await Context.Set<Weather>()
            .AsQueryable()
            .Where(x => (x.CityId == cityId) && (x.ForecastDateId == forecastDateId))
            .FirstOrDefaultAsync());
    }
}