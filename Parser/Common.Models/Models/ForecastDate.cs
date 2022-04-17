using Common.Models.Entities;

namespace Common.Models.Models;
public class ForecastDate : Entity
{
    public DateTime Date { get; set; }
    public List<Weather> Weathers { get; set; }
}