using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.AddMoviePoster;

public class AddMoviePosterHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService) : IRequestHandler<AddMoviePosterCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddMoviePosterCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdAsync(request.Id);

        if (movie is null) return Result<Unit>.Failure("Movie not found.", 404);

        var uploadResult = await photoService.UploadPhotoAsync(request.File);

        if (uploadResult is null) return Result<Unit>.Failure("Failed to upload photo", 400);

        var poster = new MoviePoster
        {
            MovieId = movie.Id,
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url
        };

        movie.PosterUrl ??= poster.Url;

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to save photo to database.", 400);
    }
}
