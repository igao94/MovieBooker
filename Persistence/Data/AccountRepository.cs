using Domain.Common.Constants;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class AccountRepository(UserManager<User> userManager) : IAccountRepository
{
    public async Task<IdentityResult> RegisterUserAsync(User user, string password)
    {
        return await userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        return await userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await userManager.Users.AnyAsync(u => u.UserName == username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> IsUserAdmin(User user)
    {
        return await userManager.IsInRoleAsync(user, UserRoles.Admin);
    }
}
