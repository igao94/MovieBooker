using Domain.Entities;

namespace Persistence.Specifications.ShowtimeSeatReservationsSpecification;

public class ShowtimeSeatReservationSpecification : BaseSpecification<ShowtimeSeatReservation>
{
    public ShowtimeSeatReservationSpecification(string showtimeSeatId, DateTime reservedDate)
        : base(sr => sr.ShowtimeSeatId == showtimeSeatId && sr.ReservedDate == reservedDate)
    {

    }
}
