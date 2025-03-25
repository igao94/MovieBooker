using Application.Showtimes.DTOs;
using FluentValidation;

namespace Application.Showtimes.Validators;

public class BaseShowtimeValidator<T, TDto> : AbstractValidator<T> where TDto : BaseShowtimeDto
{
    public BaseShowtimeValidator(Func<T, TDto> selector)
    {
        RuleFor(st => selector(st).AvailableSeats)
            .NotEmpty().WithMessage("Seats are required.");

        RuleFor(st => selector(st).StartTime)
            .Must(startTime => startTime > DateTime.UtcNow)
                .WithMessage("Start time must be in the future.")
            .Must(startTime => startTime.Kind == DateTimeKind.Utc)
                .WithMessage("Start time must be in UTC format.");
    }
}
