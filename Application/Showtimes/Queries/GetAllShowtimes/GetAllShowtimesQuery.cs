using Application.Core;
using Application.Showtimes.ShowtimeDTOs;
using MediatR;

namespace Application.Showtimes.Queries.GetAllShowtimes;

public class GetAllShowtimesQuery : IRequest<Result<IReadOnlyList<ShowtimeDto>>>
{

}
