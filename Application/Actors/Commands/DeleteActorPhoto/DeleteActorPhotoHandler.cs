using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ActorsSpecification;

namespace Application.Actors.Commands.DeleteActorPhoto;

public class DeleteActorPhotoHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService) : IRequestHandler<DeleteActorPhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteActorPhotoCommand request, CancellationToken cancellationToken)
    {
        var spec = new ActorPhotoSpecification(request.PhotoId);

        var photo = await unitOfWork.Repository<ActorPhoto>().GetEntityWithSpecAsync(spec);

        if (photo is null) return Result<Unit>.Failure("Photo not found.", 404);

        await photoService.DeletePhotoAsync(photo.PublicId);

        if (!string.IsNullOrEmpty(photo.Actor.PictureUrl)) photo.Actor.PictureUrl = null;

        unitOfWork.Repository<ActorPhoto>().Remove(photo);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete photo.", 400);
    }
}
