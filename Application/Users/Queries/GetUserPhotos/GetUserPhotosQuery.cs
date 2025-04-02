using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetUserPhotos;

public class GetUserPhotosQuery(string userId) : IRequest<Result<IReadOnlyList<UserPhotoDto>>>
{
    public string UserId { get; set; } = userId;
}
