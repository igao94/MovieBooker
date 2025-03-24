using Application.Core;
using Application.Interfaces;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.MoviesSpecification;

namespace Application.Movies.Queries.GetAllMovies;

public class GetAllMoviesHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllMoviesQuery, Result<IReadOnlyList<MovieDto>>>
{
    public async Task<Result<IReadOnlyList<MovieDto>>> Handle(GetAllMoviesQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        var IsAdmin = await unitOfWork.AccountRepository.IsUserAdmin(user);

        if (IsAdmin)
        {
            var specWithIgnoreFilter = new MovieWithIgnoreQueryFilterSpecification(request.SpecParams);

            var moviesWithIgnoreFilter = await unitOfWork.Repository<Movie>()
                .GetEntitiesWithSpecAsync(specWithIgnoreFilter);

            return Result<IReadOnlyList<MovieDto>>
                .Success(mapper.Map<IReadOnlyList<MovieDto>>(moviesWithIgnoreFilter));
        }

        var spec = new MovieSpecification(request.SpecParams);

        var movies = await unitOfWork.Repository<Movie>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<MovieDto>>.Success(mapper.Map<IReadOnlyList<MovieDto>>(movies));
    }
}
