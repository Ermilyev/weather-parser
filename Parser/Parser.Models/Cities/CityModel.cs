using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Parser.Models.Cities;

public record CityModel()
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; } = Guid.Empty;
    [Required]
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}
