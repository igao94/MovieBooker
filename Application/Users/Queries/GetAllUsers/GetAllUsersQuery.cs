using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery(UserParams userParams) : IRequest<Result<IReadOnlyList<UserDto>>>
{
    public UserParams UserParams { get; set; } = userParams;
}
