using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Commands.AddMovie;

public class AddMovieCommand(CreateMovieDto movieDto) : IRequest<Result<MovieDto>>
{
    public CreateMovieDto MovieDto { get; set; } = movieDto;
}
