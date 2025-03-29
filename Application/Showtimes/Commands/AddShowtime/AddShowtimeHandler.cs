using Application.Core;
using Application.Showtimes.ShowtimeDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Specifications.ShowtimesSpecification;

namespace Application.Showtimes.Commands.AddShowtime;

public class AddShowTimeHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<AddShowtimeCommand, Result<ShowtimeDto>>
{
    public async Task<Result<ShowtimeDto>> Handle(AddShowtimeCommand request,
        CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdAsync(request.CreateShowtimeDto.MovieId);

        if (movie is null) return Result<ShowtimeDto>.Failure("Movie not found.", 404);

        if (!movie.IsActive) return Result<ShowtimeDto>.Failure("Movie not active.", 400);

        var startTime = request.CreateShowtimeDto.StartTime;

        var endTime = request.CreateShowtimeDto.EndTime;

        if (endTime <= startTime) return Result<ShowtimeDto>
                .Failure("End time must be greater than start time.", 400);

        var spec = new ExistingShowtimeSpecification(request.CreateShowtimeDto.MovieId);

        var showtime = await unitOfWork.Repository<Showtime>().GetEntityWithSpecAsync(spec);

        if (showtime is not null) return Result<ShowtimeDto>
                .Failure("Showtime already exist for this movie.", 400);

        showtime = new Showtime
        {
            MovieId = movie.Id,
            StartTime = startTime,
            EndTime = endTime
        };

        unitOfWork.Repository<Showtime>().Add(showtime);

        var seats = Enumerable.Range(1, request.CreateShowtimeDto.AvailableSeats)
            .Select(seatNumber => new ShowtimeSeat
            {
                ShowtimeId = showtime.Id,
                SeatNumber = seatNumber
            })
            .ToList();

        unitOfWork.Repository<ShowtimeSeat>().AddRange(seats);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<ShowtimeDto>.Success(mapper.Map<ShowtimeDto>(showtime))
            : Result<ShowtimeDto>.Failure("Failed to create showtime.", 400);
    }
}
