using Application.Actors.DTOs;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Actors.Commands.AddPhotoToActor;

public class AddPhotoToActorCommand() : IRequest<Result<ActorPhotoDto>>
{
    public required string ActorId { get; set; }
    public required IFormFile File { get; set; }
}
