using Application.Actors.Commands.CreateActor;
using Application.Actors.DTOs;

namespace Application.Actors.Validators;

public class CreateActorValidator : BaseActorValidator<CreateActorCommand, CreateActorDto>
{
    public CreateActorValidator() : base(a => a.CreateActorDto)
    {

    }
}
