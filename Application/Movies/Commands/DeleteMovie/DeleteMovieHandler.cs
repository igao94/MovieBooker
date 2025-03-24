using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.MoviesSpecification;

namespace Application.Movies.Commands.DeleteMovie;

public class DeleteMovieHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMovieCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var spec = new MovieSpecification(request.Id);

        var movie = await unitOfWork.Repository<Movie>().GetEntityWithSpecAsync(spec);

        if (movie is null) return Result<Unit>.Failure("Movie not found.", 404);

        unitOfWork.Repository<MovieActor>().DeleteRange(movie.Actors);

        unitOfWork.Repository<Movie>().Remove(movie);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete movie.", 400);
    }
}
