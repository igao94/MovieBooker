using Application.Showtimes.Commands.UpdateShowtime;
using FluentValidation;

namespace Application.Showtimes.Validators;

public class UpdateShowtimeValidator : AbstractValidator<UpdateShowtimeCommand>
{
    public UpdateShowtimeValidator()
    {
        RuleFor(st => st.UpdateShowtimeDto.StartTime)
            .Must(startTime => startTime > DateTime.UtcNow)
                .WithMessage("Start time must be in the future.")
            .Must(startTime => startTime.Kind == DateTimeKind.Utc)
                .WithMessage("Start time must be in UTC format.");

        RuleFor(st => st.UpdateShowtimeDto.AvailableSeats)
            .NotEmpty().WithMessage("Seats are required.");
    }
}
