namespace Application.Showtimes.DTOs;

public class CreateShowtimeDto : BaseShowtimeDto
{
    public string MovieId { get; set; } = string.Empty;
}
