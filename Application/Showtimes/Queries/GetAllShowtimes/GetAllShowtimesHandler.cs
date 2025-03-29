using Application.Core;
using Application.Showtimes.ShowtimeDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimesSpecification;

namespace Application.Showtimes.Queries.GetAllShowtimes;

public class GetAllShowtimesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllShowtimesQuery, Result<IReadOnlyList<ShowtimeDto>>>
{
    public async Task<Result<IReadOnlyList<ShowtimeDto>>> Handle(GetAllShowtimesQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ShowtimeWithMoviesSpecification();

        var showtimes = await unitOfWork.Repository<Showtime>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<ShowtimeDto>>.Success(mapper.Map<IReadOnlyList<ShowtimeDto>>(showtimes));
    }
}
