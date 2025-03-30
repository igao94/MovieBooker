using Domain.Entities;

namespace Persistence.Specifications.ShowtimeSeatsSpecification;

public class ShowtimeSeatWithShowtimeSpecifcation : BaseSpecification<ShowtimeSeat>
{
    public ShowtimeSeatWithShowtimeSpecifcation(string id) : base(ss => ss.Id == id)
    {
        AddInclude(ss => ss.Showtime);
    }
}
