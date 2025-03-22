using Application.Actors.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Actors.Commands.CreateActor;

public class CreateActorHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateActorCommand, Result<ActorDto>>
{
    public async Task<Result<ActorDto>> Handle(CreateActorCommand request, CancellationToken cancellationToken)
    {
        var actor = mapper.Map<Actor>(request.CreateActorDto);

        unitOfWork.Repository<Actor>().Add(actor);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<ActorDto>.Success(mapper.Map<ActorDto>(actor))
            : Result<ActorDto>.Failure("Failed to create an actor.", 400);
    }
}
