using Application.Core;
using Application.Showtimes.ShowtimeSeatDTOs;
using Azure.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimeSeatReservationsSpecification;
using Persistence.Specifications.ShowtimeSeatsSpecification;

namespace Application.Showtimes.Queries.GetAvailableSeats;

public class GetAvailableSeatsHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAvailableSeatsQuery, Result<IReadOnlyList<SeatDto>>>
{
    public async Task<Result<IReadOnlyList<SeatDto>>> Handle(GetAvailableSeatsQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await ValidateShowtimeAndDateAsync(request.ShowtimeId, request.Date);

        var seats = await GetAvailableSeatsAsync(request.ShowtimeId, request.Date);

        return validationResult.IsSuccess
            ? Result<IReadOnlyList<SeatDto>>.Success(seats)
            : Result<IReadOnlyList<SeatDto>>.Failure(validationResult.Error!, validationResult.StatusCode);
    }

    private async Task<Result<bool>> ValidateShowtimeAndDateAsync(string showtimeId, DateTime date)
    {
        var showtime = await unitOfWork.Repository<Showtime>().GetByIdAsync(showtimeId);

        if (showtime is null) return Result<bool>.Failure("Showtime not found.", 404);

        var showtimeStartTime = showtime.StartTime.TimeOfDay;

        if (date.TimeOfDay != showtimeStartTime)
            return Result<bool>
                .Failure($"Please choose a valid time, movie starts at: {showtime.StartTime.TimeOfDay}.", 400);

        if (date < showtime.StartTime || date > showtime.EndTime)
            return Result<bool>.Failure("Date is out of showtime range.", 400);

        return Result<bool>.Success(true);
    }

    private async Task<IReadOnlyList<SeatDto>> GetAvailableSeatsAsync(string showtimeId, DateTime date)
    {
        var spec = new ShowtimeSeatNumberSpecification(showtimeId);

        var seats = await unitOfWork.Repository<ShowtimeSeat>().GetEntitiesWithSpecAsync(spec);

        var reservedSpec = new ShowtimeSeatReservationSpecification(showtimeId, date);

        var reservedSeats = await unitOfWork.Repository<ShowtimeSeatReservation>()
            .GetEntitiesWithSpecAsync(reservedSpec);

        var availableSeats = FilterAvailableSeats(seats, reservedSeats);

        return availableSeats;
    }

    private static List<SeatDto> FilterAvailableSeats(IReadOnlyList<ShowtimeSeat> seats,
        IReadOnlyList<ShowtimeSeatReservation> reservedSeats)
    {
        return seats
            .Where(seat => !reservedSeats.Any(rs => rs.ShowtimeSeatId == seat.Id))
            .Select(s => new SeatDto
            {
                Id = s.Id,
                SeatNumber = s.SeatNumber,
                Price = s.Price,
                Currency = s.Currency
            })
            .OrderBy(seat => seat.SeatNumber)
            .ToList();
    }
}
