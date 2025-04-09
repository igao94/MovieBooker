using Application.Core;
using MediatR;

namespace Application.Actors.Commands.DeleteActorPhoto;

public class DeleteActorPhotoCommand(string photoId) : IRequest<Result<Unit>>
{
    public string PhotoId { get; set; } = photoId;
}
