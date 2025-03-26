namespace Application.Showtimes.ShowtimeSeatDTOs;

public class ShowtimeSeatDto
{
    public string Id { get; set; } = string.Empty;
    public int SeatNumber { get; set; }
    public bool IsReserved { get; set; }
}
