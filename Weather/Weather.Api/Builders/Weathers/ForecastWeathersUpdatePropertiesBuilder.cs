using System.Linq.Expressions;
using Common.Infrastructure.Builders;
using Weather.Api.Models.Weathers;
using Weather.Domain.Entities.Weathers;

namespace Weather.Api.Builders.Weathers;

public class ForecastWeathersUpdatePropertiesBuilder(UpdateForecastWeatherModel model) 
    : UpdatePropertiesBuilder<ForecastWeather>
{
    public override IEnumerable<Expression<Func<ForecastWeather, object>>> BuildProperties()
    {
        var properties = new List<Expression<Func<ForecastWeather, object>>>();

        if (model.CityId != null)
        {
            properties.Add(weather => weather.CityId);
        }
        
        if (model.DateId != null)
        {
            properties.Add(weather => weather.DateId);
        }
        
        if (model.Name != null)
        {
            properties.Add(weather => weather.Name);
        }
        
        return properties;
    }
}