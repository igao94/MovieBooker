using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQuery(string userId) : IRequest<Result<UserDto>>
{
    public string UserId { get; set; } = userId;
}
