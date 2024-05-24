using System.ComponentModel.DataAnnotations;
using Weather.Api.Validators.Cities;

namespace Weather.Api.Models.Cities;

public sealed class CreateForecastCityModel(string name)  : IValidatableObject
{
    [Required, MinLength(1), MaxLength(100)]
    public required string Name { get; init; } = ToUpperFirstLetter(name);
    
    private static string ToUpperFirstLetter(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        var trim = name.Trim();
        return char.ToUpper(trim[0]) + trim[1..];
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return CityNameValidator.Validate(Name);
    }
}