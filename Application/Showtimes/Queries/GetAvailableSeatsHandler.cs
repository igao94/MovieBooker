using Application.Core;
using Application.Showtimes.ShowtimeSeatDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimeSeatsSpecification;

namespace Application.Showtimes.Queries;

public class GetAvailableSeatsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAvailableSeatsQuery, Result<IReadOnlyList<SeatDto>>>
{
    public async Task<Result<IReadOnlyList<SeatDto>>> Handle(GetAvailableSeatsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ShowtimeSeatNumberSpecification(request.ShowtimeId);

        var seats = await unitOfWork.Repository<ShowtimeSeat>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<SeatDto>>.Success(mapper.Map<IReadOnlyList<SeatDto>>(seats));
    }
}
