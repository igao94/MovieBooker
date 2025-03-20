using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.Data;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<AppDbContext>();


        return services;
    }
}
