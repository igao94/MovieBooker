using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.ToggleActiveMovie;

public class ToggleActiveMovieHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ToggleActiveMovieCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ToggleActiveMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdWithIgnoreFilterAsync(request.Id);

        if (movie is null) return Result<Unit>.Failure("Movie not found.", 404);

        movie.IsActive = !movie.IsActive;

        var result = await unitOfWork.CompleteAsync();

        return result
        ? Result<Unit>.Success(Unit.Value)
        : Result<Unit>.Failure("Failed to update movie status.", 400);
    }
}
