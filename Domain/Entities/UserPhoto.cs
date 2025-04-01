namespace Domain.Entities;

public class UserPhoto : BaseEntity
{
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public User User { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
