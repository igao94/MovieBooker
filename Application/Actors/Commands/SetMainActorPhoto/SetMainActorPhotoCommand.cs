using Application.Core;
using MediatR;

namespace Application.Actors.Commands.SetMainActorPhoto;

public class SetMainActorPhotoCommand(string actorId, string photoId) : IRequest<Result<Unit>>
{
    public string ActorId { get; set; } = actorId;
    public string PhotoId { get; set; } = photoId;
}
