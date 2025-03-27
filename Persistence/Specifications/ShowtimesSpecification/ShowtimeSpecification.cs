using Domain.Entities;

namespace Persistence.Specifications.ShowtimesSpecification;

public class ShowtimeSpecification : BaseSpecification<Showtime>
{
    public ShowtimeSpecification(string id) : base(st => st.Id == id)
    {

    }
}
