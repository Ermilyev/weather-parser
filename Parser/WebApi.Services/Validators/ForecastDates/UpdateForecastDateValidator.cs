using FluentValidation;
using WebApi.Models.ForecastDates;

namespace WebApi.Services.Validators.ForecastDates
{
    public class UpdateForecastDateValidator : AbstractValidator<UpdateForecastDateModel>
    {
        public UpdateForecastDateValidator()
        {
            RuleFor(p => p.Date)
                .Must(IsValidDate).WithMessage("{PropertyName} is required.");
        }

        private static bool IsValidDate(DateTime date) => date.Equals(default) == false;
    }
}
