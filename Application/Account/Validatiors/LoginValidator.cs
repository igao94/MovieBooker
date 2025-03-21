using Application.Account.Command.Login;
using FluentValidation;

namespace Application.Account.Validatiors;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(u => u.LoginDto.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        RuleFor(u => u.LoginDto.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}
