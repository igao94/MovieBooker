using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.SetMainPoster;

public class SetMainPosterHandler(IUnitOfWork unitOfWork) : IRequestHandler<SetMainPosterCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SetMainPosterCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdAsync(request.MovieId);

        if (movie is null) return Result<Unit>.Failure("Movie not found.", 404);

        var poster = await unitOfWork.Repository<MoviePoster>().GetByIdAsync(request.PosterId);

        if (poster is null) return Result<Unit>.Failure("Poster not found.", 404);

        if (movie.PosterUrl == poster.Url) return Result<Unit>.Failure("Poster already selected.", 400);

        movie.PosterUrl = poster.Url;

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to change poster.", 400);
    }
}
