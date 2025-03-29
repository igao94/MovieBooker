using Application.Actors.Commands.AddActorToMovie;
using FluentValidation;

namespace Application.Actors.Validators;

public class AddActorToMovieValidator : AbstractValidator<AddActorToMovieCommand>
{
    public AddActorToMovieValidator()
    {
        RuleFor(a => a.ActorId)
            .NotEmpty().WithMessage("ActorId is required.");        
        
        RuleFor(a => a.MovieId)
            .NotEmpty().WithMessage("MovieId is required.");
    }
}
