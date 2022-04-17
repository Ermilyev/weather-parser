using FluentValidation;
using WebApi.Models.Cities;

namespace WebApi.Services.Validators.Cities
{
    public class CreateCityValidator : AbstractValidator<CreateCityModel>
    {
        public CreateCityValidator()
        {
            RuleFor(p => p.Name).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Must(IsValidName).WithMessage("{PropertyName} should be all letters.");
        }

        private static bool IsValidName(string name) => name.Any(char.IsNumber) == false;
    }
}
