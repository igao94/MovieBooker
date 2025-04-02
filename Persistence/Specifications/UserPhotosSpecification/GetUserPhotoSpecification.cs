using Domain.Entities;

namespace Persistence.Specifications.UserPhotosSpecification;

public class GetUserPhotoSpecification : BaseSpecification<UserPhoto>
{
    public GetUserPhotoSpecification(string userId) : base(up => up.UserId == userId)
    {
        AddInclude(up => up.User);

        AddOrderBy(up => up.CreationDate);
    }
}
