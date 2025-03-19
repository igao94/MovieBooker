using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Queries.GetAllMovies;

public class GetAllMoviesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllMoviesQuery, Result<IReadOnlyList<MovieDto>>>
{
    public async Task<Result<IReadOnlyList<MovieDto>>> Handle(GetAllMoviesQuery request,
        CancellationToken cancellationToken)
    {
        var movies = await unitOfWork.Repository<Movie>().GetAllAsync();

        return Result<IReadOnlyList<MovieDto>>.Success(mapper.Map<IReadOnlyList<MovieDto>>(movies));
    }
}
