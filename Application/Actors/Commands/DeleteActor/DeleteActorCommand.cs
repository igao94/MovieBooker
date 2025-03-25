using Application.Core;
using MediatR;

namespace Application.Actors.Commands.DeleteActor;

public class DeleteActorCommand(string id) : IRequest<Result<Unit>>
{
    public string Id { get; set; } = id;
}
