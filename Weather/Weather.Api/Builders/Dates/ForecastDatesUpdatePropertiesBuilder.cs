using System.Linq.Expressions;
using Common.Infrastructure.Builders;
using Weather.Api.Models.Dates;
using Weather.Domain.Entities.Dates;

namespace Weather.Api.Builders.Dates;

public class ForecastDatesUpdatePropertiesBuilder(UpdateForecastDateModel model) 
    : UpdatePropertiesBuilder<ForecastDate>
{
    public override IEnumerable<Expression<Func<ForecastDate, object>>> BuildProperties()
    {
        var properties = new List<Expression<Func<ForecastDate, object>>>();

        if (model.Day != null)
        {
            properties.Add(date => date.Day);
        }
        
        return properties;
    }
}