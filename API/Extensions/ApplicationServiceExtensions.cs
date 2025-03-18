using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

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

        return services;
    }
}
