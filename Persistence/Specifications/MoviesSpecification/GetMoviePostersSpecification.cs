using Domain.Entities;

namespace Persistence.Specifications.MoviesSpecification;

public class GetMoviePostersSpecification : BaseSpecification<MoviePoster>
{
    public GetMoviePostersSpecification(string movieId) : base(mp => mp.MovieId == movieId) 
    {
        
    }
}
