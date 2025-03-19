using Application.Core;
using MediatR;

namespace Application.Movies.Commands.DeleteMovie;

public class DeleteMovieCommand(string id) : IRequest<Result<Unit>>
{
    public string Id { get; set; } = id;
}
