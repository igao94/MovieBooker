using Domain.Entities;

namespace Persistence.Specifications.ActorsSpecification;

public class ActorPhotoSpecification : BaseSpecification<ActorPhoto>
{
    public ActorPhotoSpecification(string photoId) : base(ap => ap.Id == photoId)
    {
        AddInclude(ap => ap.Actor);
    }
}
