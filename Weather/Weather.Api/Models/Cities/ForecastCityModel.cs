using System.ComponentModel.DataAnnotations;

namespace Weather.Api.Models.Cities;

public sealed class ForecastCityModel : EntityModel
{
    [Required, MinLength(1), MaxLength(100)]
    public required string Name { get; init; }
}