namespace Domain.Entities;

public class ShowtimeSeat : BaseEntity
{
    public string ShowtimeId { get; set; } = null!;
    public Showtime Showtime { get; set; } = null!;
    public int SeatNumber { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public ICollection<ShowtimeSeatReservation> Reservations { get; set; } = [];
}
