using Application.Core;
using Application.Interfaces;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.MoviesSpecification;

namespace Application.Movies.Queries.GetMovieById;

public class GetMovieByIdHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetMovieByIdQuery, Result<MovieDto>>
{
    public async Task<Result<MovieDto>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        var IsAdmin = await unitOfWork.AccountRepository.IsUserAdmin(user);

        if (IsAdmin)
        {
            var specWithIgnoreFilter = new MovieWithIgnoreQueryFilterSpecification(request.Id);

            var movieWithIgnoreFilter = await unitOfWork.Repository<Movie>()
                .GetEntityWithSpecAsync(specWithIgnoreFilter);

            return Result<MovieDto>.Success(mapper.Map<MovieDto>(movieWithIgnoreFilter));
        }

        var spec = new MovieSpecification(request.Id);

        var movie = await unitOfWork.Repository<Movie>().GetEntityWithSpecAsync(spec);

        if (movie is null) return Result<MovieDto>.Failure("Movie not found.", 404);

        return Result<MovieDto>.Success(mapper.Map<MovieDto>(movie));
    }
}
