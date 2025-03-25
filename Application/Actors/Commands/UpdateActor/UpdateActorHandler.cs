using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Actors.Commands.UpdateActor;

public class UpdateActorHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateActorCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
    {
        var actor = await unitOfWork.Repository<Actor>().GetByIdAsync(request.UpdateActorDto.Id);

        if (actor is null) return Result<Unit>.Failure("No actor found.", 404);

        mapper.Map(request.UpdateActorDto, actor);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update an actor.", 400);
    }
}
