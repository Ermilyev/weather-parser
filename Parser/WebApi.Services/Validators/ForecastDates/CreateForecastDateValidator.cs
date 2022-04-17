using FluentValidation;
using WebApi.Models.ForecastDates;

namespace WebApi.Services.Validators.ForecastDates
{
   public class CreateForecastDateValidator : AbstractValidator<CreateForecastDateModel>
    {
        public CreateForecastDateValidator()
        {
            RuleFor(p => p.Date).Cascade(CascadeMode.Stop)
                .Must(IsValidDate).WithMessage("{PropertyName} is required.");
        }

        private static bool IsValidDate(DateTime date) => date.Equals(default) == false;
    }
}
