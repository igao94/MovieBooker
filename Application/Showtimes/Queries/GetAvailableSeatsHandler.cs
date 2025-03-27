using Application.Core;
using Application.Showtimes.ShowtimeSeatDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimeSeatsSpecification;

namespace Application.Showtimes.Queries;

public class GetAvailableSeatsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAvailableSeatsQuery, Result<IReadOnlyList<ShowtimeSeatDto>>>
{
    public async Task<Result<IReadOnlyList<ShowtimeSeatDto>>> Handle(GetAvailableSeatsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ShowtimeSeatSpecification(request.ShowtimeId);

        var seats = await unitOfWork.Repository<ShowtimeSeat>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<ShowtimeSeatDto>>
            .Success(mapper.Map<IReadOnlyList<ShowtimeSeatDto>>(seats));
    }
}
