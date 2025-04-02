using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.Data;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(string userId) => await context.Users.FindAsync(userId);
}
