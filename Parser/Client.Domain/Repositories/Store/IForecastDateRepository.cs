using Client.Models.ForecastDate;

namespace Client.Domain.Repositories.Store;

public interface IForecastDateRepository
{
    Task<IReadOnlyList<ForecastDateModel>> GetByFilter(DateTime[]? dates);
    Task<ForecastDateModel> GetForecastDate(Guid id);
}