using Application.Actors.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ActorsSpecification;

namespace Application.Actors.Queries.GetActorPhotos;

public class GetActorPhotosHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetActorPhotosQuery, Result<IReadOnlyList<ActorPhotoDto>>>
{
    public async Task<Result<IReadOnlyList<ActorPhotoDto>>> Handle(GetActorPhotosQuery request, 
        CancellationToken cancellationToken)
    {
        var spec = new GetActorPhotosSpecification(request.ActorId);

        var photos = await unitOfWork.Repository<ActorPhoto>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<ActorPhotoDto>>.Success(mapper.Map<IReadOnlyList<ActorPhotoDto>>(photos));
    }
}
