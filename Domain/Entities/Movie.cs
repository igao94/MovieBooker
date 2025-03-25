namespace Domain.Entities;

public class Movie : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public string Director { get; set; } = string.Empty;
    public string? Language { get; set; }
    public string Duration { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public ICollection<MovieActor> Actors { get; set; } = [];
    public ICollection<Showtime> ShowTimes { get; set; } = [];
}
