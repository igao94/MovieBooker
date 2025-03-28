using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public ICollection<ShowtimeSeatReservation> SeatsReserved { get; set; } = [];
}
