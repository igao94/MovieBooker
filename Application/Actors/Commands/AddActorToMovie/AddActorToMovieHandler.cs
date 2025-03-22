using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Actors.Commands.AddActorToMovie;

public class AddActorToMovieHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddActorToMovieCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddActorToMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdAsync(request.MovieId);

        if (movie is null) return Result<Unit>.Failure("Movie not found.", 404);

        var actor = await unitOfWork.Repository<Actor>().GetByIdAsync(request.ActorId);

        if (actor is null) return Result<Unit>.Failure("Actor not found.", 404);

        var existingActor = await unitOfWork.Repository<MovieActor>().GetByCompositeKeyAsync(movie.Id, actor.Id);

        if (existingActor is not null)
            return Result<Unit>.Failure("Actor is already assigned to this movie.", 400);

        var movieActor = new MovieActor
        {
            MovieId = movie.Id,
            ActorId = actor.Id
        };

        unitOfWork.Repository<MovieActor>().Add(movieActor);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to create an actor.", 400);
    }
}
