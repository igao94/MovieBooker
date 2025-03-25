using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimesSpecification;

namespace Application.Showtimes.Commands.DeleteShowtime;

public class DeleteShowtimeHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteShowtimeCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteShowtimeCommand request, CancellationToken cancellationToken)
    {
        var spec = new ShowtimeSpecification(request.Id);

        var showtime = await unitOfWork.Repository<Showtime>().GetEntityWithSpecAsync(spec);

        if (showtime is null) return Result<Unit>.Failure("Showtime not found.", 404);

        unitOfWork.Repository<Showtime>().Remove(showtime);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete showtime.", 400);
    }
}
