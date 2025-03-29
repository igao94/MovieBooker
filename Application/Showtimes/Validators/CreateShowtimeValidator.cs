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
            .NotEmpty().WithMessage("StartTime is required.")
            .Must(time => time > DateTime.UtcNow).WithMessage("Start time must be in the future.")
            .Must(time => time.Kind == DateTimeKind.Utc).WithMessage("Start time must be in UTC format.");    
        
        RuleFor(st => st.CreateShowtimeDto.EndTime)
            .NotEmpty().WithMessage("EndTime is required.")
            .Must(time => time > DateTime.UtcNow).WithMessage("EndTime time must be in the future.")
            .Must(time => time.Kind == DateTimeKind.Utc).WithMessage("EndTime time must be in UTC format.");        

        RuleFor(st => st.CreateShowtimeDto.AvailableSeats)
            .NotEmpty().WithMessage("Seats are required.");
    }
}
