using System.ComponentModel.DataAnnotations;

namespace Weather.Api.Models;

public abstract class EntityModel
{
    [Required]
    public required Guid Id  { get; init; }
}