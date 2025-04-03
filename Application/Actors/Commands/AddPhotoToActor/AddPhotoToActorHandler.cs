using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Actors.Commands.AddPhotoToActor;

public class AddPhotoToActorHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService) : IRequestHandler<AddPhotoToActorCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddPhotoToActorCommand request, CancellationToken cancellationToken)
    {
        var actor = await unitOfWork.Repository<Actor>().GetByIdAsync(request.ActorId);

        if (actor is null) return Result<Unit>.Failure("Actor not found.", 404);

        var uploadResult = await photoService.UploadPhotoAsync(request.File);

        if (uploadResult is null) return Result<Unit>.Failure("Failed to upload photo.", 400);

        var photo = new ActorPhoto
        {
            ActorId = actor.Id,
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url
        };

        actor.PictureUrl ??= photo.Url;

        unitOfWork.Repository<ActorPhoto>().Add(photo);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to save photo to database.", 400);
    }
}
