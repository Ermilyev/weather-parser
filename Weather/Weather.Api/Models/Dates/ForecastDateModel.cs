using System.ComponentModel.DataAnnotations;

namespace Weather.Api.Models.Dates;

public sealed class ForecastDateModel : EntityModel
{
    [Required] 
    public required DateTime Day { get; init; } 
}