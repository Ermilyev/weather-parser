using System.Linq.Expressions;
using Common.Infrastructure.Builders;
using Weather.Api.Models.Cities;
using Weather.Domain.Entities.Cities;

namespace Weather.Api.Builders.Cities;

public class ForecastCitiesFilterPropertiesBuilder(ForecastCitiesFilter filter)
    : FilterPropertiesBuilder<ForecastCity>
{
    public override Expression<Func<ForecastCity, bool>> BuildProperties()
    {
        var predicate = PropertiesBuilder.True<ForecastCity>();

        if (filter.Ids != null && filter.Ids.Count != 0)
        {
            predicate = predicate.And(city => filter.Ids.Contains(city.Id));
        }

        if (filter.Names != null && filter.Names.Count != 0)
        {
            predicate = predicate.And(city => filter.Names.Contains(city.Name));
        }

        return predicate;
    }
}