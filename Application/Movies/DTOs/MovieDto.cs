using Application.Actors.DTOs;

namespace Application.Movies.DTOs;

public class MovieDto : BaseMovieDto
{
    public string Id { get; set; } = string.Empty;
    public ICollection<ActorDto> Actors { get; set; } = [];
}
