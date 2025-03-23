using Application.Core;
using Application.Movies.DTOs;
using MediatR;
using Persistence.Specifications.MoviesSpecification;

namespace Application.Movies.Queries.GetAllMovies;

public class GetAllMoviesQuery(MovieSpecParams specParams) : IRequest<Result<IReadOnlyList<MovieDto>>>
{
    public MovieSpecParams SpecParams { get; set; } = specParams;
}
