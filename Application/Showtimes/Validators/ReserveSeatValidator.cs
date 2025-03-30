using Application.Showtimes.Commands.ReserveSeat;
using FluentValidation;

namespace Application.Showtimes.Validators;

public class ReserveSeatValidator : AbstractValidator<ReserveSeatCommand>
{
    public ReserveSeatValidator()
    {
        RuleFor(r => r.ShowtimeSeatId)
            .NotEmpty().WithMessage("ShowtimeSeatId is required.");

        RuleFor(r => r.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Must(date => date > DateTime.UtcNow).WithMessage("Date must be in the future.")
            .Must(date => date.Kind == DateTimeKind.Utc).WithMessage("Date must be in UTC format.");
    }
}
