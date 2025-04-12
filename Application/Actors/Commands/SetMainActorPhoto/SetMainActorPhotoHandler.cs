using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Actors.Commands.SetMainActorPhoto;

public class SetMainActorPhotoHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<SetMainActorPhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SetMainActorPhotoCommand request, CancellationToken cancellationToken)
    {
        var actor = await unitOfWork.Repository<Actor>().GetByIdAsync(request.ActorId);

        if (actor is null) return Result<Unit>.Failure("Actor not found.", 404);

        var photo = await unitOfWork.Repository<ActorPhoto>().GetByIdAsync(request.PhotoId);

        if (photo is null) return Result<Unit>.Failure("Photo not found.", 404);

        if (photo.Url == actor.PictureUrl) return Result<Unit>.Failure("Already main photo.", 400);

        actor.PictureUrl = photo.Url;

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to change main photo.", 400);
    }
}
