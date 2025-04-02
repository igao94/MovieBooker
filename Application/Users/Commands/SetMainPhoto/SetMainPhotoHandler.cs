using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.SetMainPhoto;

public class SetMainPhotoHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<SetMainPhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserWithPhotosAsync();

        var photo = user.UserPhotos.FirstOrDefault(up => up.Id == request.PhotoId);

        if (photo is null) return Result<Unit>.Failure("Photo not found.", 404);

        if (photo.Url == user.PictureUrl) return Result<Unit>.Failure("Photo is already main.", 400);

        user.PictureUrl = photo.Url;

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to set main photo.", 400);
    }
}
