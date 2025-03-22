using Application.Account.DTOs;
using Application.Core;
using Application.Interfaces;
using MediatR;

namespace Application.Account.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoHandler(IUserAccessor userAccessor)
    : IRequestHandler<GetCurrentUserInfoQuery, Result<CurrentUserDto>>
{
    public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        if (user.Email is null || user.UserName is null)
            return Result<CurrentUserDto>.Failure("No username or email found.", 400);

        return Result<CurrentUserDto>.Success(new CurrentUserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName,
            PictureUrl = user.PictureUrl
        });
    }
}
