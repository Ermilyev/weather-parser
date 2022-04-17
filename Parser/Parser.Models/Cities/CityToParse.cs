using System.ComponentModel.DataAnnotations;

namespace Parser.Models.Cities;

public record CityToParse(string Name, string Link)
{ 
    [Required] public string Name { get; set; } = Name;
    [Required] public string Link { get; set; } = Link;
    public string TenDaysLink() => $"https://www.gismeteo.ru{Link}10-days";
}