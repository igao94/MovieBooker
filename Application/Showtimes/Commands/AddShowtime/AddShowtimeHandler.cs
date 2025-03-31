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
        var movieValidation = await ValidateMovieAsync(request);

        if (!movieValidation.IsSuccess) 
            return Result<ShowtimeDto>.Failure(movieValidation.Error!, movieValidation.StatusCode);

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
            MovieId = request.CreateShowtimeDto.MovieId,
            StartTime = startTime,
            EndTime = endTime,
        };

        unitOfWork.Repository<Showtime>().Add(showtime);

        AddSeats(request, showtime);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<ShowtimeDto>.Success(mapper.Map<ShowtimeDto>(showtime))
            : Result<ShowtimeDto>.Failure("Failed to create showtime.", 400);
    }

    private async Task<Result<bool>> ValidateMovieAsync(AddShowtimeCommand request)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdAsync(request.CreateShowtimeDto.MovieId);

        if (movie is null) return Result<bool>.Failure("Movie not found.", 404);

        if (!movie.IsActive) return Result<bool>.Failure("Movie is not active.", 400);

        return Result<bool>.Success(true);
    }

    private void AddSeats(AddShowtimeCommand request, Showtime showtime)
    {
        var seats = Enumerable.Range(1, request.CreateShowtimeDto.AvailableSeats)
            .Select(seatNumber => new ShowtimeSeat
            {
                ShowtimeId = showtime.Id,
                SeatNumber = seatNumber
            })
            .ToList();

        unitOfWork.Repository<ShowtimeSeat>().AddRange(seats);
    }
}
