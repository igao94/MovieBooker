using Application.Core;
using Application.Showtimes.ShowtimeSeatDTOs;
using MediatR;

namespace Application.Showtimes.Queries;

public class GetAvailableSeatsQuery(string showtimeId) : IRequest<Result<IReadOnlyList<SeatDto>>>
{
    public string ShowtimeId { get; set; } = showtimeId;
}
