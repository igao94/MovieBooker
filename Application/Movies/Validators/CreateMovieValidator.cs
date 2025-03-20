using Application.Movies.Commands.AddMovie;
using Application.Movies.DTOs;

namespace Application.Movies.Validators;

public class CreateMovieValidator : BaseMovieValidator<AddMovieCommand, CreateMovieDto>
{
    public CreateMovieValidator() : base(m => m.MovieDto)
    {

    }
}
