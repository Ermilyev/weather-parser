using Common.Domain.Entities;
using Weather.Domain.Entities.Weathers;

namespace Weather.Domain.Entities.Dates;

public sealed class ForecastDate : Entity
{
    public required DateTime Day { get; set; }
    
    #region Relationships

    public IReadOnlyList<ForecastWeather>? Weather { get; init; }
    
    #endregion
}