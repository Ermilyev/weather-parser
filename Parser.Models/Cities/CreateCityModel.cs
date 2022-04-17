using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Parser.Models.Cities;

public record CreateCityModel(string Name)
{
    [Required]
    [JsonProperty("name")]
    public string Name { get; set; } = Name;
}