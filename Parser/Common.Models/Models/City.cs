using Common.Models.Entities;

namespace Common.Models.Models;

public class City : Entity
{
    public string Name { get; set; }
    public List<Weather> Weathers { get; set; }
}