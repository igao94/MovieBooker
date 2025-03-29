using Domain.Entities;

namespace Persistence.Specifications.ShowtimesSpecification;

public class ShowtimeWithMoviesSpecification : BaseSpecification<Showtime>
{
    public ShowtimeWithMoviesSpecification() : base(null)
    {
        AddInclude(st => st.Movie);

        AddOrderBy(st => st.Movie.ReleaseDate);
    }

    public ShowtimeWithMoviesSpecification(string id) : base(st => st.Id == id)
    {
        AddInclude(st => st.Movie);
    }
}
