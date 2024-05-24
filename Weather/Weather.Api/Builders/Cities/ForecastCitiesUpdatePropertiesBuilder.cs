using System.Linq.Expressions;
using Common.Infrastructure.Builders;
using Weather.Api.Models.Cities;
using Weather.Domain.Entities.Cities;

namespace Weather.Api.Builders.Cities;

public class ForecastCitiesUpdatePropertiesBuilder(UpdateForecastCityModel model) 
    : UpdatePropertiesBuilder<ForecastCity>
{
    public override IEnumerable<Expression<Func<ForecastCity, object>>> BuildProperties()
    {
        var properties = new List<Expression<Func<ForecastCity, object>>>();

        if (model.Name != null)
        {
            properties.Add(city => city.Name);
        }
        
        return properties;
    }
}