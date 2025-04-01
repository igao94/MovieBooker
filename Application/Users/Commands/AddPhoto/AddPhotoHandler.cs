using Application.Core;
using Application.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.AddPhoto;

public class AddPhotoHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IPhotoService photoService,
    IMapper mapper) : IRequestHandler<AddPhotoCommand, Result<UserPhotoDto>>
{
    public async Task<Result<UserPhotoDto>> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        var uploadResult = await photoService.UploadPhotoAsync(request.File);

        if (uploadResult is null) return Result<UserPhotoDto>.Failure("Failed to upload photo.", 400);

        var photo = new UserPhoto
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url,
            UserId = user.Id,
        };

        user.PictureUrl ??= photo.Url;

        unitOfWork.Repository<UserPhoto>().Add(photo);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<UserPhotoDto>.Success(mapper.Map<UserPhotoDto>(photo))
            : Result<UserPhotoDto>.Failure("Problem saving photo to database.", 400);
    }
}
