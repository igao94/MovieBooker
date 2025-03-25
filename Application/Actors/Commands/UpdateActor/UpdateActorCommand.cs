using Application.Actors.DTOs;
using Application.Core;
using MediatR;

namespace Application.Actors.Commands.UpdateActor;

public class UpdateActorCommand(UpdateActorDto updateActorDto) : IRequest<Result<Unit>>
{
    public UpdateActorDto UpdateActorDto { get; set; } = updateActorDto;
}
