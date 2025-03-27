using Application.Core;
using Application.Showtimes.ShowtimeDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

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

        var endTime = startTime.AddDays(6);

        var showTime = new Showtime
        {
            MovieId = movie.Id,
            StartTime = startTime,
            EndTime = endTime
        };

        unitOfWork.Repository<Showtime>().Add(showTime);

        var seats = Enumerable.Range(1, request.CreateShowtimeDto.AvailableSeats)
            .Select(seatNumber => new ShowtimeSeat
            {
                ShowtimeId = showTime.Id,
                SeatNumber = seatNumber,
                IsReserved = false
            })
            .ToList();

        unitOfWork.Repository<ShowtimeSeat>().AddRange(seats);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<ShowtimeDto>.Success(mapper.Map<ShowtimeDto>(showTime))
            : Result<ShowtimeDto>.Failure("Failed to create showtime.", 400);
    }
}
