using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IPhotoService photoService) : IRequestHandler<DeleteUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserWithPhotosAsync();

        if (user.UserPhotos.Count != 0)
        {
            var publicIds = user.UserPhotos.Select(up => up.PublicId).ToList();

            await photoService.DeletePhotosAsync(publicIds);
        }

        unitOfWork.UserRepository.DeleteUser(user);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.", 400);
    }
}
