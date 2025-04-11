using Application.Core;
using Application.Interfaces;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.AddMoviePoster;

public class AddMoviePosterHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService,
    IMapper mapper) : IRequestHandler<AddMoviePosterCommand, Result<MoviePosterDto>>
{
    public async Task<Result<MoviePosterDto>> Handle(AddMoviePosterCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdAsync(request.Id);

        if (movie is null) return Result<MoviePosterDto>.Failure("Movie not found.", 404);

        var uploadResult = await photoService.UploadPhotoAsync(request.File);

        if (uploadResult is null) return Result<MoviePosterDto>.Failure("Failed to upload photo", 400);

        var poster = new MoviePoster
        {
            MovieId = movie.Id,
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url
        };

        movie.PosterUrl ??= poster.Url;

        unitOfWork.Repository<MoviePoster>().Add(poster);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<MoviePosterDto>.Success(mapper.Map<MoviePosterDto>(poster))
            : Result<MoviePosterDto>.Failure("Failed to save photo to database.", 400);
    }
}
