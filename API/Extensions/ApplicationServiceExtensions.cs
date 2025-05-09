﻿using API.Middleware;
using Application.Core;
using Application.Interfaces;
using Application.Movies.Queries.GetAllMovies;
using Application.Movies.Validators;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Security;
using Infrastructure.Services;
using Infrastructure.Services.Photos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            opt.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddDbContext<AppDbContext>(options =>
        {
            var useInMemoryDatabase = config.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                options.UseInMemoryDatabase("InMemoryDatabase");
            }
            else
            {
                options.UseSqlServer(config.GetConnectionString("SqlServer"));
            }
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddHttpContextAccessor();

        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetAllMoviesQuery).Assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddTransient<ExceptionMiddleware>();

        services.AddValidatorsFromAssemblyContaining<CreateMovieValidator>();

        services.AddHostedService<MovieDeactivationService>();

        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

        services.AddScoped<IPhotoService, PhotoService>();

        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}
