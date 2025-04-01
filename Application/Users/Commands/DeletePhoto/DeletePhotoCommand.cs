using Application.Core;
using MediatR;

namespace Application.Users.Commands.DeletePhoto;

public class DeletePhotoCommand(string photoId) : IRequest<Result<Unit>>
{
    public string PhotoId { get; set; } = photoId;
}
