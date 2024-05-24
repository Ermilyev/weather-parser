using Common.Domain.Primitives.Results;
using Common.Domain.ValueObjects;
using Weather.Api.Mappers;
using Weather.Api.Models.Dates;
using Weather.Domain.Repositories.Dates;

namespace Weather.Api.Services;

public sealed class ForecastDateService(
    IForecastDateReadRepository readRepository, 
    IForecastDateWriteRepository writeRepository)
{
    public async Task<Result<PaginatedForecastDates>> GetAllAsync(ForecastDatesFilter filter, PaginationRecord pagination, CancellationToken ct)
    {
        var find = filter.AsFind(pagination);
        var (weathers, total) = await readRepository.GetByFilterAsync(find, ct);

        if (total == 0)
        {
            Result.Ok(PaginatedForecastDates.Empty());
        }

        var paginatedWeathers = new PaginatedForecastDates
        {
            Items = weathers.AsModels(),
            Total = total
        };

        return Result.Ok(paginatedWeathers);
    }

    public async Task<Result<ForecastDateModel>> GetAsync(Guid id, CancellationToken ct)
    {
        var date = await readRepository.GetByIdAsync(id, ct);

        return date == null
            ? Result.NotFound<ForecastDateModel>()
            : Result.Ok(date.AsModel());
    }

    public async Task<Result<ForecastDateModel>>  CreateAsync(CreateForecastDateModel createModel, CancellationToken ct)
    {
        if (await readRepository.ExistsWithDayAsync(createModel.Day, ct))
        {
            return Result.Conflict<ForecastDateModel>();
        }

        var date = createModel.AsDate();
        await writeRepository.InsertAsync(date, ct);
        return Result.Ok(date.AsModel());
    }

    public async Task<Result<ForecastDateModel>>  UpdateAsync(Guid id, UpdateForecastDateModel updateModel, CancellationToken ct)
    {
        var date = await readRepository.GetByIdAsync(id, ct);

        if (date == null)
        {
            return Result.NotFound<ForecastDateModel>();
        }
        
        if (updateModel.Day != null && updateModel.Day.Equals(date.Day))
        {
            return Result.Ok(date.AsModel());
        }
        
        if (updateModel.Day != null && await readRepository.ExistsWithDayAsync(updateModel.Day.Value, ct))
        {
            return Result.Conflict<ForecastDateModel>();
        }
        
        var update = updateModel.AsUpdate();
        
        date.ApplyUpdates(updateModel);
        
        var updatedDate = await writeRepository.UpdateAsync(date, update, ct);

        return Result.Ok(updatedDate.AsModel());
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