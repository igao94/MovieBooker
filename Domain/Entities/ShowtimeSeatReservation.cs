namespace Domain.Entities;

public class ShowtimeSeatReservation : BaseEntity
{
    public string ShowtimeSeatId { get; set; } = null!;
    public ShowtimeSeat ShowtimeSeat { get; set; } = null!;
    public DateTime ReservedDate { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public bool IsReserved { get; set; }
}
