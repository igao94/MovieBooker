using Application.Core;
using MediatR;

namespace Application.Users.Commands.SetMainPhoto;

public class SetMainPhotoCommand(string photoId) : IRequest<Result<Unit>>
{
    public string PhotoId { get; set; } = photoId;
}
