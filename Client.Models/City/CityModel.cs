using System.ComponentModel.DataAnnotations;

namespace Client.Models.City;

public record CityModel
{
    public CityModel()
    {
    }

    public CityModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    [Required] public string Name { get; set; }

    public void Deconstruct(out Guid cityId, out string cityName)
    {
        cityId = Id;
        cityName = Name;
    }
}
