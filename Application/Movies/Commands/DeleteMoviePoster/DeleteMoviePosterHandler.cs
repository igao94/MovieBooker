using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.MoviesSpecification;

namespace Application.Movies.Commands.DeleteMoviePoster;

public class DeleteMoviePosterHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService) : IRequestHandler<DeleteMoviePosterCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMoviePosterCommand request, CancellationToken cancellationToken)
    {
        var spec = new MoviePosterSpecification(request.PosterId);

        var poster = await unitOfWork.Repository<MoviePoster>().GetEntityWithSpecAsync(spec);

        if (poster is null) return Result<Unit>.Failure("Poster not found.", 404);

        await photoService.DeletePhotoAsync(poster.PublicId);

        if (!string.IsNullOrEmpty(poster.Movie.PosterUrl)) poster.Movie.PosterUrl = null;

        unitOfWork.Repository<MoviePoster>().Remove(poster);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete poster.", 400);
    }
}
