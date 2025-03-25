using Application.Core;
using Application.Showtimes.DTOs;
using MediatR;

namespace Application.Showtimes.Commands.UpdateShowtime;

public class UpdateShowtimeCommand(UpdateShowtimeDto updateShowtimeDto) : IRequest<Result<Unit>>
{
    public UpdateShowtimeDto UpdateShowtime { get; set; } = updateShowtimeDto;
}
