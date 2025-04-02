using Application.Account.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Account.Commands.Login;

public class LoginHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<LoginCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository.GetUserByEmailAsync(request.LoginDto.Email);

        if (user is null || user.UserName is null || user.Email is null)
            return Result<AccountDto>.Failure("User not found.", 404);

        var result = await unitOfWork.AccountRepository.CheckPasswordAsync(user, request.LoginDto.Password);

        if (!result) return Result<AccountDto>.Failure("Invalid email or password.", 400);

        var token = await tokenService.GetTokenAsync(user);

        return Result<AccountDto>.Success(new AccountDto
        {
            Id = user.Id,
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Token = token,
            PictureUrl = user?.PictureUrl
        });
    }
}
