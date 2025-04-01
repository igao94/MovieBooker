using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.DeletePhoto;

public class DeletePhotoHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IPhotoService photoService) : IRequestHandler<DeletePhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserWithPhotosAsync();

        var photo = user.UserPhotos.FirstOrDefault(up => up.Id == request.PhotoId);

        if (photo is null) return Result<Unit>.Failure("Photo not found.", 404);

        if (photo.Url == user.PictureUrl) return Result<Unit>.Failure("Can't delete main photo.", 400);

        await photoService.DeletePhotoAsync(photo.PublicId);

        user.UserPhotos.Remove(photo);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete photo.", 400);
    }
}
