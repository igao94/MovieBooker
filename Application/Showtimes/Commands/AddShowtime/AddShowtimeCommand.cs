using Application.Core;
using Application.Showtimes.ShowtimeDTOs;
using MediatR;

namespace Application.Showtimes.Commands.AddShowtime;

public class AddShowtimeCommand(CreateShowtimeDto createShowtimeDto) : IRequest<Result<ShowtimeDto>>
{
    public CreateShowtimeDto CreateShowtimeDto { get; set; } = createShowtimeDto;
}
