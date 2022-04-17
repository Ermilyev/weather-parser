using FluentValidation;
using WebApi.Models.Cities;

namespace WebApi.Services.Validators.Cities
{
    public class UpdateCityValidator : AbstractValidator<UpdateCityModel>
    {
        public UpdateCityValidator()
        {
            When(c => string.IsNullOrWhiteSpace(c.Name), () =>
            {
                RuleFor(p => p.Name).Cascade(CascadeMode.Stop).NotEmpty()
                                    .WithMessage("{PropertyName} should be not empty.")
                                    .Must(IsValidName).WithMessage("{PropertyName} should be all letters.");
            });
        }

        private static bool IsValidName(string name) => name.Any(char.IsNumber) == false;
    }
}
