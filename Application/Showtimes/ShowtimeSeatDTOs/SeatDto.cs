namespace Application.Showtimes.ShowtimeSeatDTOs;

public class SeatDto
{
    public string Id { get; set; } = string.Empty;
    public int SeatNumber { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
}
