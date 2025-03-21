using API.Extensions;
using API.Middleware;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();

    var userManager = services.GetRequiredService<UserManager<User>>();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    var useInMemoryDatabase = services.GetRequiredService<IConfiguration>()
        .GetValue<bool>("UseInMemoryDatabase");

    if (!useInMemoryDatabase) await context.Database.MigrateAsync();

    await Seed.SeedDataAsync(context, userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();

    logger.LogError(ex, "An error occurred durning migration.");
}

app.Run();
