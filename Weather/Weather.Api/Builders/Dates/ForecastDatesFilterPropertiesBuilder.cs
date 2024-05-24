using System.Linq.Expressions;
using Common.Infrastructure.Builders;
using Weather.Api.Models.Dates;
using Weather.Domain.Entities.Dates;

namespace Weather.Api.Builders.Dates;

public class ForecastDatesFilterPropertiesBuilder(ForecastDatesFilter filter)
    : FilterPropertiesBuilder<ForecastDate>
{
    public override Expression<Func<ForecastDate, bool>> BuildProperties()
    {
        var predicate = PropertiesBuilder.True<ForecastDate>();

        if (filter.Ids != null && filter.Ids.Count != 0)
        {
            predicate = predicate.And(date => filter.Ids.Contains(date.Id));
        }

        if (filter.Days != null && filter.Days.Count != 0)
        {
            predicate = predicate.And(date => filter.Days.Contains(date.Day));
        }

        return predicate;
    }
}