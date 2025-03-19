using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Queries.GetAllMovies;

public class GetAllMoviesQuery : IRequest<Result<IReadOnlyList<MovieDto>>>
{
}
