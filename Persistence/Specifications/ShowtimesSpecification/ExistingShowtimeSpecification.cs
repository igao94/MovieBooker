using Domain.Entities;

namespace Persistence.Specifications.ShowtimesSpecification;

public class ExistingShowtimeSpecification : BaseSpecification<Showtime>
{
    public ExistingShowtimeSpecification(string movieId) : base(st => st.MovieId == movieId)
    {

    }
}
