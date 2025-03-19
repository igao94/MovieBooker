using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Queries.GetMovieById;

public class GetMovieByIdQuery(string id) : IRequest<Result<MovieDto>>
{
    public string Id { get; set; } = id;
}