namespace Application.Actors.DTOs;

public class ActorDto : BaseActorDto
{
    public string Id {  get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
}
