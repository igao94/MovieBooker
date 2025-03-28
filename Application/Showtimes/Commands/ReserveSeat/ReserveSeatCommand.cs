using Application.Core;
using MediatR;

namespace Application.Showtimes.Commands.ReserveSeat;

public class ReserveSeatCommand(string showtimeId, string showtimeSeatId, DateTime date) : IRequest<Result<Unit>>
{
    public string ShowtimeId { get; set; } = showtimeId;
    public string ShowtimeSeatId { get; set; } = showtimeSeatId;
    public DateTime Date { get; set; } = date;
}
