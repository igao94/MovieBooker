namespace Application.Users;

public class UserParams
{
    private string? _search;

    public string Search
    {
        get => _search ?? string.Empty;

        set => _search = value.ToLower();
    }
}
