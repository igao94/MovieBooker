using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public void DeleteUser(User user) => context.Users.Remove(user);

    public async Task<IReadOnlyList<User>> GetAllAsync(string? search)
    {
        var query = context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = context.Users
                .Where(u => u.UserName!.ToLower().Contains(search) ||
                    u.Email!.Contains(search) ||
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search));
        }

        return await query.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(string userId) => await context.Users.FindAsync(userId);
}
