using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimeSeatsSpecification;

namespace Application.Showtimes.Queries;

public class GetAvailableSeatsHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAvailableSeatsQuery, Result<IReadOnlyList<int>>>
{
    public async Task<Result<IReadOnlyList<int>>> Handle(GetAvailableSeatsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ShowtimeSeatNumberSpecification(request.ShowtimeId);

        var seats = await unitOfWork.Repository<ShowtimeSeat>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<int>>.Success(seats);
    }
}
