using Domain.Entities;

namespace Persistence.Specifications.ShowtimeSeatsSpecification;

public class ShowtimeSeatSpecification : BaseSpecification<ShowtimeSeat>
{
    public ShowtimeSeatSpecification(string showtimeId)
        : base(ss => ss.ShowtimeId == showtimeId && !ss.IsReserved)
    {
        AddOrderBy(ss => ss.SeatNumber);
    }

    public ShowtimeSeatSpecification(string showtimeId, int seatNumber)
        : base(ss => ss.ShowtimeId == showtimeId && ss.SeatNumber == seatNumber)
    {

    }
}
