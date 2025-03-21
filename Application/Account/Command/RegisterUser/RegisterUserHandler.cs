using Application.Account.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Common.Constants;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Account.Command.RegisterUser;

public class RegisterUserHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<RegisterUserCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.AccountRepository.EmailExistsAsync(request.RegisterDto.Email))
            return Result<AccountDto>.Failure("Email is already taken.", 400);

        if (await unitOfWork.AccountRepository.UsernameExistsAsync(request.RegisterDto.Username))
            return Result<AccountDto>.Failure("Username is already taken.", 400);

        var user = new User
        {
            FirstName = request.RegisterDto.FirstName,
            LastName = request.RegisterDto.LastName,
            Email = request.RegisterDto.Email,
            UserName = request.RegisterDto.Username
        };

        var result = await unitOfWork.AccountRepository.RegisterUserAsync(user, request.RegisterDto.Password);

        if (!result.Succeeded) return Result<AccountDto>.Failure("Failed to create user.", 400);

        var roleResult = await unitOfWork.AccountRepository.AddToRoleAsync(user, UserRoles.User);

        if (!roleResult.Succeeded) return Result<AccountDto>.Failure("Failed to add to role.", 400);

        var token = await tokenService.GetTokenAsync(user);

        return Result<AccountDto>.Success(new AccountDto
        {
            Email = user.Email,
            Username = user.UserName,
            Token = token,
            PictureUrl = null
        });
    }
}
