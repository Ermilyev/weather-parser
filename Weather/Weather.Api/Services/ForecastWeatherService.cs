using Common.Domain.Primitives.Results;
using Common.Domain.ValueObjects;
using Weather.Api.Mappers;
using Weather.Api.Models.Weathers;
using Weather.Domain.Repositories.Weathers;

namespace Weather.Api.Services;

public sealed class ForecastWeatherService(
    IForecastWeatherReadRepository readRepository, 
    IForecastWeatherWriteRepository writeRepository)
{
    public async Task<Result<PaginatedForecastWeathers>> GetAllAsync(ForecastWeathersFilter filter, PaginationRecord pagination, CancellationToken ct)
    {
        var find = filter.AsFind(pagination);
        var (weathers, total) = await readRepository.GetByFilterAsync(find, ct);

        if (total == 0)
        {
            Result.Ok(PaginatedForecastWeathers.Empty());
        }

        var paginatedDates = new PaginatedForecastWeathers
        {
            Items = weathers.AsModels(),
            Total = total
        };

        return Result.Ok(paginatedDates);
    }

    public async Task<Result<ForecastWeatherModel>> GetAsync(Guid id, CancellationToken ct)
    {
        var weather = await readRepository.GetByIdAsync(id, ct);

        return weather == null
            ? Result.NotFound<ForecastWeatherModel>()
            : Result.Ok(weather.AsModel());
    }

    public async Task<Result<ForecastWeatherModel>> CreateAsync(CreateForecastWeatherModel createModel, CancellationToken ct)
    {
        if (await readRepository.ExistsWithParamsAsync(createModel.CityId, createModel.DateId, ct))
        {
            return Result.Conflict<ForecastWeatherModel>();
        }

        var weather = createModel.AsDate();
        await writeRepository.InsertAsync(weather, ct);
        return Result.Ok(weather.AsModel());
    }

    public async Task<Result<ForecastWeatherModel>> UpdateAsync(Guid id, UpdateForecastWeatherModel updateModel, CancellationToken ct)
    {
        var weather = await readRepository.GetByIdAsync(id, ct);

        if (weather == null)
        {
            return Result.NotFound<ForecastWeatherModel>();
        }
        
        if (updateModel is { CityId: not null, DateId: not null } && 
            updateModel.CityId.Equals(weather.CityId) && 
            updateModel.DateId.Equals(weather.DateId))
        {
            return Result.Ok(weather.AsModel());
        }
        
        if (updateModel is { CityId: not null, DateId: not null } && 
            await readRepository.ExistsWithParamsAsync(updateModel.CityId.Value, updateModel.DateId.Value, ct))
        {
            return Result.Conflict<ForecastWeatherModel>();
        }
        
        var update = updateModel.AsUpdate();
        
        weather.ApplyUpdates(updateModel);
        
        var updatedWeather = await writeRepository.UpdateAsync(weather, update, ct);

        return Result.Ok(updatedWeather.AsModel());
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