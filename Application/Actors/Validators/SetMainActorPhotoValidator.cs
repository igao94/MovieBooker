using Application.Actors.Commands.SetMainActorPhoto;
using FluentValidation;

namespace Application.Actors.Validators;

public class SetMainActorPhotoValidator : AbstractValidator<SetMainActorPhotoCommand>
{
    public SetMainActorPhotoValidator()
    {
        RuleFor(ap => ap.ActorId)
            .NotEmpty().WithMessage("ActorId is required.");

        RuleFor(ap => ap.PhotoId)
            .NotEmpty().WithMessage("PhotoId is required.");
    }
}
