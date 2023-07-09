using DriversManagement.Models.DTOs.Account;
using DriversManagement.Models.DTOs.User;
using FluentValidation;

namespace DriversManagement.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(user => user.Mobile)
                .NotNull()
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
