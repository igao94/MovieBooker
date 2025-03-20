using Application.Movies.Commands.UpdateMovie;
using Application.Movies.DTOs;

namespace Application.Movies.Validators;

public class UpdateMovieValidator : BaseMovieValidator<UpdateMovieCommand, UpdateMovieDto>
{
    public UpdateMovieValidator() : base(m => m.UpdateMovieDto)
    {

    }
}
