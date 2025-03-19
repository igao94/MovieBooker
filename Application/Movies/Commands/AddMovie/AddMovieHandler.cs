using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.AddMovie;

public class AddMovieHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<AddMovieCommand, Result<MovieDto>>
{
    public async Task<Result<MovieDto>> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = mapper.Map<Movie>(request.MovieDto);

        unitOfWork.Repository<Movie>().Add(movie);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<MovieDto>.Success(mapper.Map<MovieDto>(movie))
            : Result<MovieDto>.Failure("Failed to add movie.", 400);
    }
}
