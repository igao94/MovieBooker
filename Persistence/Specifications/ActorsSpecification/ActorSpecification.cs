using Domain.Entities;

namespace Persistence.Specifications.ActorsSpecification;

public class ActorSpecification : BaseSpecification<Actor>
{
    public ActorSpecification(string id) : base(a => a.Id == id)
    {
        AddInclude(a => a.Movies);
    }

    public ActorSpecification(ActorSpecParams specParams) : base(a =>
        (string.IsNullOrEmpty(specParams.Search) || a.FullName.ToLower().Contains(specParams.Search)))
    {
        switch (specParams.Sort)
        {
            case "actorsAsc":
                AddOrderBy(a => a.FullName);
                break;
            case "actorsDesc":
                AddOrderByDescending(a => a.FullName);
                break;
            default:
                AddOrderByDescending(a => a.Id);
                break;
        }
    }
}
