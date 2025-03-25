using Application.Showtimes.Commands.UpdateShowtime;
using Application.Showtimes.DTOs;

namespace Application.Showtimes.Validators;

public class UpdateShowtimeValidator : BaseShowtimeValidator<UpdateShowtimeCommand, UpdateShowtimeDto>
{
    public UpdateShowtimeValidator() : base(a => a.UpdateShowtimeDto)
    {

    }
}
