using Application.Users.Commands.AddPhoto;
using Application.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    [HttpPost("add-photo")]
    public async Task<ActionResult<UserPhotoDto>> AddPhoto([FromForm] AddPhotoCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }
}
