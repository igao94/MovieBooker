using System.Text.Json.Serialization;

namespace Application.Showtimes.ShowtimeDTOs;

public class UpdateShowtimeDto
{
    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
}
