using Application.Actors.DTOs;
using Application.Core;
using MediatR;

namespace Application.Actors.Queries.GetActorPhotos;

public class GetActorPhotosQuery(string actorId) : IRequest<Result<IReadOnlyList<ActorPhotoDto>>>
{
    public string ActorId { get; set; } = actorId;
}
