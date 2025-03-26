using Domain.Entities;

namespace Persistence.Specifications.MoviesSpecification;

public class MovieWithIgnoreQueryFilterSpecification : BaseSpecification<Movie>
{
    public MovieWithIgnoreQueryFilterSpecification(string id) : base(m => m.Id == id)
    {
        ApplyIgnoreGlobalQueryFilter();

        AddInclude("Showtimes.ShowtimeSeats");

        AddInclude("Actors.Actor");
    }

    public MovieWithIgnoreQueryFilterSpecification(MovieSpecParams specParams) : base(m =>
        (string.IsNullOrEmpty(specParams.Search) || m.Title.ToLower().Contains(specParams.Search)) &&
        (!specParams.Genres.Any() || specParams.Genres.Any(g => m.Genre.Contains(g))) &&
        (!specParams.Actors.Any() || m.Actors.Any(a => specParams.Actors
            .Any(actor => a.Actor.FullName.Contains(actor.Trim())))))
    {
        ApplyIgnoreGlobalQueryFilter();

        AddInclude("Showtimes.ShowtimeSeats");

        AddInclude("Actors.Actor");

        switch (specParams.Sort)
        {
            case "moviesAsc":
                AddOrderBy(m => m.ReleaseDate);
                break;
            case "moviesDesc":
                AddOrderByDescending(m => m.ReleaseDate);
                break;
            default:
                AddOrderBy(m => m.Title);
                break;
        }
    }
}
