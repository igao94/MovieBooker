using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.MoviesSpecification;

namespace Application.Movies.Queries.GetMoviePosters;

public class GetMoviePostersHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetMoviePostersQuery, Result<IReadOnlyList<MoviePosterDto>>>
{
    public async Task<Result<IReadOnlyList<MoviePosterDto>>> Handle(GetMoviePostersQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new GetMoviePostersSpecification(request.MovieId);

        var posters = await unitOfWork.Repository<MoviePoster>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<MoviePosterDto>>.Success(mapper.Map<IReadOnlyList<MoviePosterDto>>(posters));
    }
}
