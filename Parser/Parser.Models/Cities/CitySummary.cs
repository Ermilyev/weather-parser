using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Parser.Models.Cities;

public class CitySummary
{
    [Required]
    [JsonProperty("cities")]
    public CityModel[] CityModelList { get; set; } = Array.Empty<CityModel>();
    [Required]
    [JsonProperty("count")]
    public long Count { get; set; }
}