namespace Domain.Entities;

public class Movie : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public string Director { get; set; } = string.Empty;
    public string? Language { get; set; }
    public TimeSpan Duration { get; set; }
    public decimal? Rating { get; set; }
    public string? PosterUrl { get; set; }
}
