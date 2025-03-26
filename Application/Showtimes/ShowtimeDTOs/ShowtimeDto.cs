namespace Application.Showtimes.ShowtimeDTOs;

public class ShowtimeDto
{
    public string Id { get; set; } = string.Empty;
    public string Movie { get; set; } = string.Empty;
    public int AvailableSeats {  get; set; }
    public DateTime StartTime { get; set; }
}
