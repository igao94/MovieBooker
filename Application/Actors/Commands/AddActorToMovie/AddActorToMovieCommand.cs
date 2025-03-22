using Application.Core;
using MediatR;

namespace Application.Actors.Commands.AddActorToMovie;

public class AddActorToMovieCommand(string movieId, string actorId) : IRequest<Result<Unit>>
{
    public string MovieId { get; set; } = movieId;
    public string ActorId { get; set; } = actorId;
}
