using FluentValidation;
using WebApi.Models.Weathers;

namespace WebApi.Services.Validators.Weathers
{
    public class UpdateWeatherValidator : AbstractValidator<UpdateWeatherModel>
    {
        public UpdateWeatherValidator()
        {
            RuleFor(p => p.CityId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Must(IsValidGuid).WithMessage("{PropertyName} should be valid Id.")
                .When(c => IsValidGuid(c.CityId) == false);

            RuleFor(p => p.ForecastDateId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Must(IsValidGuid).WithMessage("{PropertyName} should be valid Id.")
                .When(c => IsValidGuid(c.ForecastDateId) == false);

            RuleFor(p => p.ParsedAt)
               .Must(IsValidDate).WithMessage("{PropertyName} is required.")
               .When(c => IsValidDate(c.ParsedAt) == false);
        }

        private static bool IsValidGuid(Guid id) => id != Guid.Empty;
        private static bool IsValidDate(DateTime date) => date.Equals(default) == false;
    }
}
