using Application.Core;
using Application.Showtimes.ShowtimeDTOs;
using MediatR;

namespace Application.Showtimes.Queries.GetShowtimeById;

public class GetShowtimeByIdQuery(string showtimeId) : IRequest<Result<ShowtimeDto>>
{
    public string ShowtimeId { get; set; } = showtimeId;
}
