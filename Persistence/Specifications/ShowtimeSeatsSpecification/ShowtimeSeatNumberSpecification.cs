using Domain.Entities;

namespace Persistence.Specifications.ShowtimeSeatsSpecification;

public class ShowtimeSeatNumberSpecification : BaseSpecification<ShowtimeSeat, int>
{
    public ShowtimeSeatNumberSpecification(string showtimeId)
        : base(ss => ss.ShowtimeId == showtimeId)
    {
        AddOrderBy(ss => ss.SeatNumber);
    }
}
