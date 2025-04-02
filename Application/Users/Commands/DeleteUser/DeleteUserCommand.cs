using Application.Core;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Result<Unit>>
{

}
