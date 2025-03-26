using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimeSeatsSpecification;

namespace Application.Showtimes.Commands.ReserveSeat;

public class ReserveSeatHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<ReserveSeatCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ReserveSeatCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var spec = new ShowtimeSeatSpecification(request.ShowtimeId, request.SeatNumber);

        var seat = await unitOfWork.Repository<ShowtimeSeat>().GetEntityWithSpecAsync(spec);

        if (seat is null) return Result<Unit>.Failure("Seat not found.", 404);

        if (seat.UserId == userId)
            return Result<Unit>.Failure("You have already reserved this seat.", 400);

        if (seat.IsReserved) return Result<Unit>.Failure("Seat is already reserved.", 400);

        seat.IsReserved = true;

        seat.UserId = userId;

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to reserve seat.", 400);
    }
}
