using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Config;

namespace Persistence.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
    }
}
