namespace Application.Showtimes.DTOs;

public class ShowtimeDto : BaseShowtimeDto
{
    public string Id { get; set; } = string.Empty;
    public string MovieId { get; set; } = string.Empty;
}
