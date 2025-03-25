namespace Domain.Entities;

public class Showtime : BaseEntity
{
    public string MovieId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public int AvailableSeats { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
