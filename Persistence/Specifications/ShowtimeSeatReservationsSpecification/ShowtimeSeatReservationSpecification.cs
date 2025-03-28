using Domain.Entities;

namespace Persistence.Specifications.ShowtimeSeatReservationsSpecification;

public class ShowtimeSeatReservationSpecification : BaseSpecification<ShowtimeSeatReservation>
{
    public ShowtimeSeatReservationSpecification(string showtimeId, DateTime reservedDate)
        : base(sr => sr.ShowtimeSeat.ShowtimeId == showtimeId && sr.ReservedDate == reservedDate)
    {

    }
}
