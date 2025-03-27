using Application.Core;
using MediatR;

namespace Application.Showtimes.Commands.ReserveSeat;

public class ReserveSeatCommand(string showtimeId,
    int seatNumber,
    DateTime selectedDate) : IRequest<Result<Unit>>
{
    public string ShowtimeId { get; set; } = showtimeId;
    public int SeatNumber { get; set; } = seatNumber;
    public DateTime SelectedDate { get; set; } = selectedDate;
}
