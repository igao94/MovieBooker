using Application.Showtimes.Queries.GetAvailableSeats;
using FluentValidation;

namespace Application.Showtimes.Validators;

public class GetAvailableSeatsQueryValidator : AbstractValidator<GetAvailableSeatsQuery>
{
    public GetAvailableSeatsQueryValidator()
    {
        RuleFor(s => s.ShowtimeId)
            .NotEmpty().WithMessage("ShowtimeId is required.");

        RuleFor(s => s.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Must(date => date > DateTime.UtcNow).WithMessage("Date must be in the future.")
            .Must(date => date.Kind == DateTimeKind.Utc).WithMessage("Date must be in UTC format.");
    }
}
