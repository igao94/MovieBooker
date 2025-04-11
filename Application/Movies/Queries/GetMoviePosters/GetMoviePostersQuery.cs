using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Queries.GetMoviePosters;

public class GetMoviePostersQuery(string movieId) : IRequest<Result<IReadOnlyList<MoviePosterDto>>>
{
    public string MovieId { get; set; } = movieId;
}
