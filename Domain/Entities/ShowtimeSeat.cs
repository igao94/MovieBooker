namespace Domain.Entities;

public class ShowtimeSeat : BaseEntity
{
    public string ShowtimeId { get; set; } = null!;
    public Showtime Showtime { get; set; } = null!;
    public int SeatNumber { get; set; }
    public ICollection<ShowtimeSeatReservation> Reservations { get; set; } = [];
}
