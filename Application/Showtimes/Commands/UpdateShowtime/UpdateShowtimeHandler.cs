using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Showtimes.Commands.UpdateShowtime;

public class UpdateShowtimeHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateShowtimeCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateShowtimeCommand request, CancellationToken cancellationToken)
    {
        var showtime = await unitOfWork.Repository<Showtime>().GetByIdAsync(request.UpdateShowtime.Id);

        if (showtime is null) return Result<Unit>.Failure("No showtime found.", 404);

        mapper.Map(request.UpdateShowtime, showtime);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update showtime.", 400);
    }
}
