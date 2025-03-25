using System.Text.Json.Serialization;

namespace Application.Showtimes.DTOs;

public class UpdateShowtimeDto : BaseShowtimeDto
{
    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
}
