using Domain.Entities;

namespace Persistence.Specifications.MoviesSpecification;

public class MovieSpecification : BaseSpecification<Movie>
{
    public MovieSpecification(string id) : base(m => m.Id == id)
    {
        AddInclude("Actors.Actor");
    }

    public MovieSpecification(MovieSpecParams specParams) : base(m =>
        (string.IsNullOrEmpty(specParams.Search) || m.Title.ToLower().Contains(specParams.Search)) &&
        (!specParams.Genres.Any() || specParams.Genres.Any(g => m.Genre.Contains(g))))
    {
        AddInclude(m => m.Actors);

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
