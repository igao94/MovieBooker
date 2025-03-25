using Application.Core;
using MediatR;

namespace Application.Movies.Commands.ToggleActiveMovie;

public class ToggleActiveMovieCommand(string id) : IRequest<Result<Unit>>
{
    public string Id { get; set; } = id;
}
