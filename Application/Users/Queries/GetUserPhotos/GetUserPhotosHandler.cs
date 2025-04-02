using Application.Core;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.UserPhotosSpecification;

namespace Application.Users.Queries.GetUserPhotos;

public class GetUserPhotosHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetUserPhotosQuery, Result<IReadOnlyList<UserPhotoDto>>>
{
    public async Task<Result<IReadOnlyList<UserPhotoDto>>> Handle(GetUserPhotosQuery request, 
        CancellationToken cancellationToken)
    {
        var spec = new GetUserPhotoSpecification(request.UserId);

        var photos = await unitOfWork.Repository<UserPhoto>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<UserPhotoDto>>
            .Success(mapper.Map<IReadOnlyList<UserPhotoDto>>(photos));
    }
}
