using Application.Movies.Commands.SetMainPoster;
using FluentValidation;

namespace Application.Movies.Validators;

public class MoviePosterValidator : AbstractValidator<SetMainPosterCommand>
{
    public MoviePosterValidator()
    {
        RuleFor(mp => mp.MovieId)
            .NotEmpty().WithMessage("MovieId is required.");        
        
        RuleFor(mp => mp.PosterId)
            .NotEmpty().WithMessage("PosterId is required.");
    }
}
