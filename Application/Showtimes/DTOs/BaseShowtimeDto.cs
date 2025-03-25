namespace Application.Showtimes.DTOs;

public class BaseShowtimeDto
{
    public string MovieId { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int AvailableSeats { get; set; }
}
