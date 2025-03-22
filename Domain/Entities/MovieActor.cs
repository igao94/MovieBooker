namespace Domain.Entities;

public class MovieActor : BaseEntity
{
    public string MovieId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    public string ActorId { get; set; } = null!;
    public Actor Actor { get; set; } = null!;
}
