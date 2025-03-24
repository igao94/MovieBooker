using Application.Actors.DTOs;
using Application.Core;
using MediatR;
using Persistence.Specifications.ActorsSpecification;

namespace Application.Actors.Queries.GetAllActors;

public class GetAllActorsQuery(ActorSpecParams specParams) : IRequest<Result<IReadOnlyList<ActorDto>>>
{
    public ActorSpecParams SpecParams { get; } = specParams;
}
