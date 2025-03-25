using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimesSpecification;

namespace Application.Showtimes.Commands.UpdateShowtime;

public class UpdateShowtimeHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateShowtimeCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateShowtimeCommand request, CancellationToken cancellationToken)
    {
        var spec = new ShowtimeSpecification(request.UpdateShowtimeDto.Id);

        var showtime = await unitOfWork.Repository<Showtime>().GetEntityWithSpecAsync(spec);

        if (showtime is null) return Result<Unit>.Failure("No showtime found.", 404);

        mapper.Map(request.UpdateShowtimeDto, showtime);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update showtime.", 400);
    }
}
