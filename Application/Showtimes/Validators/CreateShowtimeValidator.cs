using Application.Showtimes.Commands.AddShowtime;
using Application.Showtimes.DTOs;
using FluentValidation;

namespace Application.Showtimes.Validators;

public class CreateShowtimeValidator : BaseShowtimeValidator<AddShowtimeCommand, CreateShowtimeDto>
{
    public CreateShowtimeValidator() : base(st => st.CreateShowtimeDto)
    {
        RuleFor(st => st.CreateShowtimeDto.MovieId)
            .NotEmpty().WithMessage("Movie id is required.");
    }
}
