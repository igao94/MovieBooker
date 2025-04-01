using Application.Account.DTOs;
using Application.Core;
using MediatR;

namespace Application.Account.Commands.RegisterUser;

public class RegisterUserCommand(RegisterDto registerDto) : IRequest<Result<AccountDto>>
{
    public RegisterDto RegisterDto { get; set; } = registerDto;
}
