using Domain.Entities;

namespace Persistence.Specifications.ShowtimesSpecification;

public class ShowtimeWithMoviesSpecification : BaseSpecification<Showtime>
{
    public ShowtimeWithMoviesSpecification(string id) : base(st => st.Id == id)
    {
        AddInclude(st => st.Movie);
    }
}
