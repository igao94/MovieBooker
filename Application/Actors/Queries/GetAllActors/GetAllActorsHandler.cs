using Application.Actors.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ActorsSpecification;

namespace Application.Actors.Queries.GetAllActors;

public class GetAllActorsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllActorsQuery, Result<IReadOnlyList<ActorDto>>>
{
    public async Task<Result<IReadOnlyList<ActorDto>>> Handle(GetAllActorsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ActorSpecification(request.SpecParams);

        var actors = await unitOfWork.Repository<Actor>().GetEntitiesWithSpecAsync(spec);

        return Result<IReadOnlyList<ActorDto>>.Success(mapper.Map<IReadOnlyList<ActorDto>>(actors));
    }
}
