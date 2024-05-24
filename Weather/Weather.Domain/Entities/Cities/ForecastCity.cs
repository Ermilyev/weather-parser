using Common.Domain.Entities;
using Weather.Domain.Entities.Weathers;

namespace Weather.Domain.Entities.Cities;

public sealed class ForecastCity : Entity
{
    public required string Name { get; set; }
    
    #region Relationships

    public IReadOnlyList<ForecastWeather>? Weather { get; init; }
    
    #endregion
}