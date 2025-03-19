using Application.Core;
using Application.Movies.Queries.GetAllMovies;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddDbContext<AppDbContext>(options =>
        {
            var useInMemoryDatabase = config.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                options.UseInMemoryDatabase(config.GetConnectionString("InMemoryDatabase")!);
            }
            else
            {
                options.UseSqlServer(config.GetConnectionString("SqlServer"));
            }
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllMoviesQuery).Assembly));

        return services;
    }
}
