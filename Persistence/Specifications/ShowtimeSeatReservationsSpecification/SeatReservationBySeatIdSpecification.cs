using Domain.Entities;

namespace Persistence.Specifications.ShowtimeSeatReservationsSpecification;

public class SeatReservationBySeatIdSpecification : BaseSpecification<ShowtimeSeatReservation>
{
    public SeatReservationBySeatIdSpecification(string showtimeSeatId, DateTime date)
        : base(sr => sr.ShowtimeSeatId == showtimeSeatId && sr.ReservedDate == date)
    {

    }
}
