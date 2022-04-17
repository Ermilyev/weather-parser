using Parser.Models.Cities;

namespace Parser.Domain.Repositories;

public interface ICityRepository
{
    Task<List<CityToParse>> ParseAsync(string url);
}