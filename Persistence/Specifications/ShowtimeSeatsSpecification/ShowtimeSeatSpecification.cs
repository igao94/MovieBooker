using Domain.Entities;

namespace Persistence.Specifications.ShowtimeSeatsSpecification;

public class ShowtimeSeatSpecification : BaseSpecification<ShowtimeSeat>
{
    public ShowtimeSeatSpecification(string showtimeId, int seatNumber)
        : base(ss => ss.ShowtimeId == showtimeId && ss.SeatNumber == seatNumber)
    {

    }
}
