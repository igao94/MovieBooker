using Application.Core;
using MediatR;

namespace Application.Showtimes.Commands.ReserveSeat;

public class ReserveSeatCommand(string showtimeSeatId, DateTime date) : IRequest<Result<Unit>>
{
    public string ShowtimeSeatId { get; set; } = showtimeSeatId;
    public DateTime Date { get; set; } = date;
}
