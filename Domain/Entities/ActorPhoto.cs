namespace Domain.Entities;

public class ActorPhoto : BaseEntity
{
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public string ActorId { get; set; } = null!;
    public Actor Actor { get; set; } = null!;
}
