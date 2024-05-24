using Common.Domain.Primitives.Results;
using Common.Domain.ValueObjects;
using Weather.Api.Mappers;
using Weather.Api.Models.Cities;
using Weather.Domain.Repositories.Cities;

namespace Weather.Api.Services;

public sealed class ForecastCityService(
    IForecastCityReadRepository readRepository,
    IForecastCityWriteRepository writeRepository)
{
    public async Task<Result<PaginatedForecastCities>> GetAllAsync(ForecastCitiesFilter filter,
        PaginationRecord pagination, CancellationToken ct)
    {
        var find = filter.AsFind(pagination);
        var (cities, total) = await readRepository.GetByFilterAsync(find, ct);

        if (total == 0)
        {
            Result.Ok(PaginatedForecastCities.Empty());
        }

        var paginatedCities = new PaginatedForecastCities
        {
            Items = cities.AsModels(),
            Total = total
        };

        return Result.Ok(paginatedCities);
    }

    public async Task<Result<ForecastCityModel>> GetAsync(Guid id, CancellationToken ct)
    {
        var city = await readRepository.GetByIdAsync(id, ct);

        return city == null
            ? Result.NotFound<ForecastCityModel>()
            : Result.Ok(city.AsModel());
    }

    public async Task<Result<ForecastCityModel>> CreateAsync(CreateForecastCityModel createModel, CancellationToken ct)
    {
        if (await readRepository.ExistsWithNameAsync(createModel.Name, ct))
        {
            return Result.Conflict<ForecastCityModel>();
        }

        var city = createModel.AsCity();
        await writeRepository.InsertAsync(city, ct);
        return Result.Ok(city.AsModel());
    }

    public async Task<Result<ForecastCityModel>> UpdateAsync(Guid id, UpdateForecastCityModel updateModel,
        CancellationToken ct)
    {
        var city = await readRepository.GetByIdAsync(id, ct);

        if (city == null)
        {
            return Result.NotFound<ForecastCityModel>();
        }
        
        if (updateModel.Name != null && updateModel.Name.Equals(city.Name))
        {
            return Result.Ok(city.AsModel());
        }
        
        if (updateModel.Name != null && await readRepository.ExistsWithNameAsync(updateModel.Name, ct))
        {
            return Result.Conflict<ForecastCityModel>();
        }
        
        var update = updateModel.AsUpdate();
        
        city.ApplyUpdates(updateModel);
        
        var updatedCity = await writeRepository.UpdateAsync(city, update, ct);

        return Result.Ok(updatedCity.AsModel());
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct)
    {
        if (await readRepository.ExistsAsync(id, ct) == false)
        {
            return Result.NotFound();
        }

        return await writeRepository.DeleteAsync(id, ct)
            ? Result.Ok()
            : Result.BadRequest();
    }
}