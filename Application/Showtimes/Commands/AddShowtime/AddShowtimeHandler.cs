using Application.Core;
using Application.Showtimes.DTOs;
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

        var showTime = new Showtime
        {
            MovieId = movie.Id,
            StartTime = request.CreateShowtimeDto.StartTime,
            AvailableSeats = request.CreateShowtimeDto.AvailableSeats
        };

        unitOfWork.Repository<Showtime>().Add(showTime);

        var result = await unitOfWork.CompleteAsync();

        return result
            ? Result<ShowtimeDto>.Success(mapper.Map<ShowtimeDto>(showTime))
            : Result<ShowtimeDto>.Failure("Failed to create showtime.", 400);
    }
}
