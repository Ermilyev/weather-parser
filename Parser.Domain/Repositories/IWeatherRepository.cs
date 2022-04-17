using Parser.Models.Cities;
using Parser.Models.Weathers;

namespace Parser.Domain.Repositories;

public interface IWeatherRepository
{
    Task<List<WeatherTenDaysModel>> ParseAsync(List<CityToParse> cityModelList);
}