using Application.Core;
using Application.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Commands.AddPhoto;

public class AddPhotoCommand : IRequest<Result<UserPhotoDto>>
{
    public required IFormFile File { get; set; }
}
