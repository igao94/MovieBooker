using Application.Actors.Commands.UpdateActor;
using Application.Actors.DTOs;

namespace Application.Actors.Validators;

public class UpdateValidator : BaseActorValidator<UpdateActorCommand, UpdateActorDto>
{
    public UpdateValidator() : base(a => a.UpdateActorDto)
    {

    }
}
