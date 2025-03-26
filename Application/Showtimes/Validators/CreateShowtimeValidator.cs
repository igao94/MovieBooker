using Application.Showtimes.Commands.AddShowtime;
using FluentValidation;

namespace Application.Showtimes.Validators;

public class CreateShowtimeValidator : AbstractValidator<AddShowtimeCommand>
{
    public CreateShowtimeValidator()
    {
        RuleFor(st => st.CreateShowtimeDto.MovieId)
            .NotEmpty().WithMessage("Move id is required.");

        RuleFor(st => st.CreateShowtimeDto.StartTime)
            .Must(startTime => startTime > DateTime.UtcNow)
                .WithMessage("Start time must be in the future.")
            .Must(startTime => startTime.Kind == DateTimeKind.Utc)
                .WithMessage("Start time must be in UTC format.");

        RuleFor(st => st.CreateShowtimeDto.AvailableSeats)
            .NotEmpty().WithMessage("Seats are required.");
    }
}
