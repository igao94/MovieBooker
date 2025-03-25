using Application.Actors.DTOs;
using FluentValidation;

namespace Application.Actors.Validators;

public class BaseActorValidator<T, TDto> : AbstractValidator<T> where TDto : BaseActorDto
{
    public BaseActorValidator(Func<T, TDto> selector)
    {
        RuleFor(a => selector(a).FullName)
            .NotEmpty().WithMessage("FullName is required.");
    }
}
