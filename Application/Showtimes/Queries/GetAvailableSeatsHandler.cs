using Application.Core;
using Application.Showtimes.ShowtimeSeatDTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimeSeatsSpecification;

namespace Application.Showtimes.Queries;

public class GetAvailableSeatsHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAvailableSeatsQuery, Result<IReadOnlyList<SeatDto>>>
{
    public async Task<Result<IReadOnlyList<SeatDto>>> Handle(GetAvailableSeatsQuery request,
        CancellationToken cancellationToken)
    {
        var showtime = await unitOfWork.Repository<Showtime>().GetByIdAsync(request.ShowtimeId);

        if (showtime is null) return Result<IReadOnlyList<SeatDto>>.Failure("Showtime not found.", 404);

        var showtimeStartTime = showtime.StartTime.TimeOfDay;

        if (request.Date.TimeOfDay != showtimeStartTime)
            return Result<IReadOnlyList<SeatDto>>
                .Failure($"Please choose valid time, movie starts at {showtimeStartTime}.", 400);

        if (request.Date < showtime.StartTime || request.Date > showtime.EndTime)
            return Result<IReadOnlyList<SeatDto>>.Failure("Date is out of showtime range.", 400);

        var spec = new ShowtimeSeatNumberSpecification(request.ShowtimeId);

        var seats = await unitOfWork.Repository<ShowtimeSeat>().GetEntitiesWithSpecAsync(spec);

        var reservedSpec = new ShowtimeSeatReservationsSpecification(request.ShowtimeId, request.Date);

        var reservedSeats = await unitOfWork.Repository<ShowtimeSeatReservation>()
            .GetEntitiesWithSpecAsync(reservedSpec);

        var availableSeats = seats
            .Where(seat => reservedSeats.Count == 0 || !reservedSeats.Any(rs => rs.ShowtimeSeatId == seat.Id))
            .Select(seat => new SeatDto
            {
                Id = seat.Id,
                SeatNumber = seat.SeatNumber,
            })
            .OrderByDescending(s => s.SeatNumber)
            .ToList();

        return Result<IReadOnlyList<SeatDto>>.Success(availableSeats);
    }
}
