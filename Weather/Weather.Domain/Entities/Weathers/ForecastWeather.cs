using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Weather.Domain.Entities.Cities;
using Weather.Domain.Entities.Dates;

namespace Weather.Domain.Entities.Weathers;

public sealed class ForecastWeather : Entity
{
    public required EntityId CityId { get; set; }
    public required EntityId DateId { get; set; }
    public required string Name { get; set; }

    #region Relationships

    public ForecastCity? City { get; init; }
    public ForecastDate? Date { get; init; }
    
    #endregion
    
}