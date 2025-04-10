using Domain.Entities;

namespace Persistence.Specifications.ActorsSpecification;

public class GetActorPhotosSpecification : BaseSpecification<ActorPhoto>
{
    public GetActorPhotosSpecification(string actorId) : base(ap => ap.ActorId == actorId)
    {
        
    }
}
