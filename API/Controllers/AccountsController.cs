using Application.Account.Commands.Login;
using Application.Account.Commands.RegisterUser;
using Application.Account.DTOs;
using Application.Account.Queries.GetCurrentUserInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<AccountDto>> Register(RegisterDto registerDto)
    {
        return HandleResult(await Mediator.Send(new RegisterUserCommand(registerDto)));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AccountDto>> Login(LoginDto loginDto)
    {
        return HandleResult(await Mediator.Send(new LoginCommand(loginDto)));
    }

    [HttpGet("current-user-info")]
    public async Task<ActionResult<CurrentUserDto>> GetCurrentUserInfo()
    {
        return HandleResult(await Mediator.Send(new GetCurrentUserInfoQuery()));
    }
}
