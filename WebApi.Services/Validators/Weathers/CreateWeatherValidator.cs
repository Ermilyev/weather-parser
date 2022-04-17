using FluentValidation;
using WebApi.Models.Weathers;

namespace WebApi.Services.Validators.Weathers
{
    public class CreateWeatherValidator : AbstractValidator<CreateWeatherModel>
    {
        public CreateWeatherValidator()
        {
            RuleFor(p => p.CityId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Must(IsValidGuid).WithMessage("{PropertyName} should be valid Id.");

            RuleFor(p => p.ForecastDateId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Must(IsValidGuid).WithMessage("{PropertyName} should be valid Id.");

            RuleFor(p => p.ParsedAt).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("{PropertyName} is required.")
               .Must(IsValidDate).WithMessage("{PropertyName} is required.");
        }

        private static bool IsValidGuid(Guid id) => id != Guid.Empty;
        private static bool IsValidDate(DateTime date) => date.Equals(default) == false;
    }
}
