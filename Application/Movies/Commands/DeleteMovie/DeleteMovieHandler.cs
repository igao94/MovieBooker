using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.DeleteMovie;

public class DeleteMovieHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMovieCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdAsync(request.Id);

        if (movie is null) return Result<Unit>.Failure("Movie not found.", 404);

        unitOfWork.Repository<Movie>().Remove(movie);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete movie.", 400);
    }
}
