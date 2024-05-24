using System.Linq.Expressions;
using Common.Infrastructure.Builders;
using Weather.Api.Models.Weathers;
using Weather.Domain.Entities.Weathers;

namespace Weather.Api.Builders.Weathers;

public class ForecastWeathersFilterPropertiesBuilder(ForecastWeathersFilter filter)
    : FilterPropertiesBuilder<ForecastWeather>
{
    public override Expression<Func<ForecastWeather, bool>> BuildProperties()
    {
        var predicate = PropertiesBuilder.True<ForecastWeather>();

        if (filter.Ids != null && filter.Ids.Count != 0)
        {
            predicate = predicate.And(weather => filter.Ids.Contains(weather.Id));
        }

        if (filter.CityId != null && filter.CityId.Count != 0)
        {
            predicate = predicate.And(weather => filter.CityId.Contains(weather.CityId));
        }
        
        if (filter.DateId != null && filter.DateId.Count != 0)
        {
            predicate = predicate.And(weather => filter.DateId.Contains(weather.DateId));
        }
        
        return predicate;
    }
}