using Application.Actors.DTOs;
using Application.Core;
using MediatR;

namespace Application.Actors.Commands.CreateActor;

public class CreateActorCommand(CreateActorDto createActorDto) : IRequest<Result<ActorDto>>
{
    public CreateActorDto CreateActorDto { get; set; } = createActorDto;
}
