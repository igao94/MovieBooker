namespace Domain.Entities;

public class MoviePoster : BaseEntity
{
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public string MovieId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}
