using Domain.Entities;

namespace Persistence.Specifications.ShowtimeSeatsSpecification;

public class ShowtimeSeatReservationsSpecification : BaseSpecification<ShowtimeSeatReservation>
{
    public ShowtimeSeatReservationsSpecification(string showtimeId, DateTime dateTime)
        : base(sr => sr.ShowtimeSeat.ShowtimeId == showtimeId && sr.ReservedDate == dateTime)
    {

    }
}
