namespace Application.Users.DTOs;

public class UserPhotoDto
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
}
