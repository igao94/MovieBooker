using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ActorsSpecification;

namespace Application.Actors.Commands.DeleteActor;

public class DeleteActorHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteActorCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
    {
        var spec = new ActorSpecification(request.Id);

        var actor = await unitOfWork.Repository<Actor>().GetEntityWithSpecAsync(spec);

        if (actor is null) return Result<Unit>.Failure("Actor not found.", 404);

        unitOfWork.Repository<MovieActor>().DeleteRange(actor.Movies);

        unitOfWork.Repository<Actor>().Remove(actor);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete actor.", 400);
    }
}
