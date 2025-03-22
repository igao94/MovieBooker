using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Persistence.Data;
using System.Security.Claims;

namespace Infrastructure.Security;

public class UserAccessor(IHttpContextAccessor httpContextAccessor, AppDbContext context) : IUserAccessor
{
    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("No user found.");
    }

    public async Task<User> GetUserAsync()
    {
        return await context.Users.FindAsync(GetUserId())
            ?? throw new UnauthorizedAccessException("No user logged in.");
    }
}
