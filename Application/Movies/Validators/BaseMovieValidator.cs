using Application.Movies.DTOs;
using FluentValidation;

namespace Application.Movies.Validators;

public class BaseMovieValidator<T, TDto> : AbstractValidator<T> where TDto : BaseMovieDto
{
    public BaseMovieValidator(Func<T, TDto> selector)
    {
        RuleFor(m => selector(m).Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(m => selector(m).Genre)
            .NotEmpty().WithMessage("Genre is required.");

        RuleFor(m => selector(m).Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(m => selector(m).ReleaseDate)
            .NotEmpty().WithMessage("ReleaseDate is required.");

        RuleFor(m => selector(m).Director)
            .NotEmpty().WithMessage("Director is required.");

        RuleFor(m => selector(m).Language)
            .NotEmpty().WithMessage("Language is required.");
    }
}
