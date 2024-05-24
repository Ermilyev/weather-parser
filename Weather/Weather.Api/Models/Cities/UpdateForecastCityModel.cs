using System.ComponentModel.DataAnnotations;
using Weather.Api.Validators.Cities;

namespace Weather.Api.Models.Cities;

public sealed class UpdateForecastCityModel(string? name) : IValidatableObject
{
    [MinLength(1), MaxLength(100)]
    public string? Name { get; } = ToUpperFirstLetter(name);
    
    private static string? ToUpperFirstLetter(string? name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        var trim = name.Trim();
        return char.ToUpper(trim[0]) + trim[1..];
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return Name != null 
            ? CityNameValidator.Validate(Name) :
            Array.Empty<ValidationResult>();
    }
}