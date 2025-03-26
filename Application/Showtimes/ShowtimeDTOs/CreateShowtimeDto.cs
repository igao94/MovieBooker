namespace Application.Showtimes.ShowtimeDTOs;

public class CreateShowtimeDto
{
    public string MovieId { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int AvailableSeats { get; set; }
}
