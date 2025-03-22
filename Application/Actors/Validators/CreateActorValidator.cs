using Application.Actors.Commands.CreateActor;
using FluentValidation;

namespace Application.Actors.Validators;

public class CreateActorValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorValidator()
    {
        RuleFor(a => a.CreateActorDto.FullName)
            .NotEmpty().WithMessage("FullName is required."); ;
    }
}
