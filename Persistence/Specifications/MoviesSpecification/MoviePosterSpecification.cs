using Domain.Entities;

namespace Persistence.Specifications.MoviesSpecification;

public class MoviePosterSpecification : BaseSpecification<MoviePoster>
{
    public MoviePosterSpecification(string posterId) : base(mp => mp.Id == posterId) 
    {
        AddInclude(mp => mp.Movie);
    }
}
