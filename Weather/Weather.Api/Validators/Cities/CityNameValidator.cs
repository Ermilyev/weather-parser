using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Weather.Api.Validators.Cities;

public sealed partial class CityNameValidator
{
    public static IEnumerable<ValidationResult> Validate(string name)
    {
        if (CityNameRegex().IsMatch(name))
        {
            yield return new ValidationResult("Цифры запрещены.", new[] { nameof(name) });
        }
    }

    [GeneratedRegex(@"\d")]
    private static partial Regex CityNameRegex();
}