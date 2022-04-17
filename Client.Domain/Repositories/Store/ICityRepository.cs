using Client.Models.City;

namespace Client.Domain.Repositories.Store;

public interface ICityRepository
{
    Task<CitySummary> GetByFilter(Guid[]? ids = null, string[]? names = null);
    Task<CityModel> GetCity(Guid id);
}