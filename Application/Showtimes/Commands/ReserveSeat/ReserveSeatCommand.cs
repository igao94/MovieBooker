using Application.Core;
using MediatR;

namespace Application.Showtimes.Commands.ReserveSeat;

public class ReserveSeatCommand(string showtimeSeatId, DateTime date, decimal price) : IRequest<Result<string>>
{
    public string ShowtimeSeatId { get; set; } = showtimeSeatId;
    public DateTime Date { get; set; } = date;
    public decimal Price { get; set; } = price;
}
