namespace Domain.Entities;

public class ShowTime : BaseEntity
{
    public string MovieId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public int AvailableSeats { get; set; }
}
