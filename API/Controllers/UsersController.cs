﻿using Application.Users;
using Application.Users.Commands.AddPhoto;
using Application.Users.Commands.DeletePhoto;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.SetMainPhoto;
using Application.Users.DTOs;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUserPhotos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    [HttpGet("get-all-users")]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUsers([FromQuery] UserParams userParams)
    {
        return HandleResult(await Mediator.Send(new GetAllUsersQuery(userParams)));
    }

    [HttpGet("get-user/{userId}")]
    public async Task<ActionResult<UserDto>> GetUserById(string userId)
    {
        return HandleResult(await Mediator.Send(new GetUserByIdQuery(userId)));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser()
    {
        return HandleResult(await Mediator.Send(new DeleteUserCommand()));
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<UserPhotoDto>> AddPhoto([FromForm] AddPhotoCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(string photoId)
    {
        return HandleResult(await Mediator.Send(new DeletePhotoCommand(photoId)));
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(string photoId)
    {
        return HandleResult(await Mediator.Send(new SetMainPhotoCommand(photoId)));
    }

    [HttpGet("user-photos/{userId}")]
    public async Task<ActionResult<IReadOnlyList<UserPhotoDto>>> GetUserPhotos(string userId)
    {
        return HandleResult(await Mediator.Send(new GetUserPhotosQuery(userId)));
    }
}
