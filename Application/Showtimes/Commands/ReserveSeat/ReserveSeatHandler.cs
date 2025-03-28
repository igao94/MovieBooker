using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimeSeatReservationsSpecification;

namespace Application.Showtimes.Commands.ReserveSeat;

public class ReserveSeatHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<ReserveSeatCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ReserveSeatCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var seat = await unitOfWork.Repository<ShowtimeSeat>().GetByIdAsync(request.ShowtimeSeatId);

        if (seat is null) return Result<Unit>.Failure("Seat not found.", 404);

        var showtime = await unitOfWork.Repository<Showtime>().GetByIdAsync(request.ShowtimeId);

        if (showtime is null) return Result<Unit>.Failure("Showtime not found.", 404);

        var showtimeStartTime = showtime.StartTime.TimeOfDay;

        if (request.Date.TimeOfDay != showtimeStartTime)
            return Result<Unit>.Failure($"Please choose valid time, movie starts at {showtimeStartTime}.", 400);

        if (request.Date < showtime.StartTime || request.Date > showtime.EndTime)
            return Result<Unit>.Failure("Date is out of showtime range.", 400);

        var spec = new ShowtimeSeatReservationSpecification(request.ShowtimeSeatId, request.Date);

        var seatReservation = await unitOfWork.Repository<ShowtimeSeatReservation>()
            .GetEntityWithSpecAsync(spec);

        if (seatReservation is not null && seatReservation.IsReserved)
            return Result<Unit>.Failure("Seat is already reserved for this date.", 400);

        var reservation = new ShowtimeSeatReservation
        {
            ShowtimeSeatId = request.ShowtimeSeatId,
            ReservedDate = request.Date,
            UserId = userId,
            IsReserved = true
        };

        unitOfWork.Repository<ShowtimeSeatReservation>().Add(reservation);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to reserve seat.", 400);
    }
}
