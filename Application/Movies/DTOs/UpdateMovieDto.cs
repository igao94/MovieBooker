using System.Text.Json.Serialization;

namespace Application.Movies.DTOs;

public class UpdateMovieDto : BaseMovieDto
{
    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
}
