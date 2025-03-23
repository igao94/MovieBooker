namespace Persistence.Specifications.MoviesSpecification;

public class MovieSpecParams
{
    private List<string> _genres = [];

    public List<string> Genres
    {
        get => _genres;

        set => _genres = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
    }

    private List<string> _actors = [];

    public List<string> Actors
    {
        get => _actors;

        set => _actors = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
    }

    private string? _search;

    public string Search
    {
        get => _search ?? string.Empty;

        set => _search = value.ToLower();
    }

    public string? Sort { get; set; }
}
