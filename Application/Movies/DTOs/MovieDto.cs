using Application.Actors.DTOs;
using Application.Showtimes.DTOs;

namespace Application.Movies.DTOs;

public class MovieDto : BaseMovieDto
{
    public string Id { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<ActorDto> Actors { get; set; } = [];
    public ICollection<ShowtimeDto> Showtimes { get; set; } = [];
}
