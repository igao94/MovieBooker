using System.Text.Json.Serialization;

namespace Application.Actors.DTOs;

public class UpdateActorDto : BaseActorDto
{
    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
}
