using Application.Account.Commands.RegisterUser;
using FluentValidation;

namespace Application.Account.Validatiors;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.RegisterDto.FirstName)
            .NotEmpty()
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("First name must start with an uppercase letter and contain only one word.");

        RuleFor(u => u.RegisterDto.LastName)
            .NotEmpty()
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("Last name must start with an uppercase letter and contain only one word.");

        RuleFor(u => u.RegisterDto.Username)
            .NotEmpty()
            .Matches(@"^[a-z0-9]+$")
            .WithMessage("Username must be a single word with only lowercase letters and numbers, without spaces.");

        RuleFor(u => u.RegisterDto.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        RuleFor(u => u.RegisterDto.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one non-alphanumeric character.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.");
    }
}
