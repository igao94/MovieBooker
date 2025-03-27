using Application.Core;
using MediatR;

namespace Application.Showtimes.Queries;

public class GetAvailableSeatsQuery(string showtimeId) : IRequest<Result<IReadOnlyList<int>>>
{
    public string ShowtimeId { get; set; } = showtimeId;
}
