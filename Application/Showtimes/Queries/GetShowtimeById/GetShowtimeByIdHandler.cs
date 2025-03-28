using Application.Core;
using Application.Showtimes.ShowtimeDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimesSpecification;

namespace Application.Showtimes.Queries.GetShowtimeById;

public class GetShowtimeByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetShowtimeByIdQuery, Result<ShowtimeDto>>
{
    public async Task<Result<ShowtimeDto>> Handle(GetShowtimeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ShowtimeWithMoviesSpecification(request.ShowtimeId);

        var showtime = await unitOfWork.Repository<Showtime>().GetEntityWithSpecAsync(spec);

        if (showtime is null) return Result<ShowtimeDto>.Failure("Showtime not found.", 404);

        return Result<ShowtimeDto>.Success(mapper.Map<ShowtimeDto>(showtime));
    }
}
