namespace Domain.Entities;

public class Actor : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public ICollection<MovieActor> Movies { get; set; } = [];
    public ICollection<ActorPhoto> Photos { get; set; } = [];
}
