using Application.Actors.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Actors.Queries;

public class GetActorByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetActorByIdQuery, Result<ActorDto>>
{
    public async Task<Result<ActorDto>> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
    {
        var actor = await unitOfWork.Repository<Actor>().GetByIdAsync(request.Id);

        if (actor is null) return Result<ActorDto>.Failure("No actor found.", 400);

        return Result<ActorDto>.Success(mapper.Map<ActorDto>(actor));
    }
}
