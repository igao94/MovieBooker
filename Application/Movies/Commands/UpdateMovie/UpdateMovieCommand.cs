using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Commands.UpdateMovie;

public class UpdateMovieCommand(UpdateMovieDto updateMovieDto) : IRequest<Result<Unit>>
{
    public UpdateMovieDto UpdateMovieDto { get; set; } = updateMovieDto;
}
