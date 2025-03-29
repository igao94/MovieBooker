using Domain.Common.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class Seed
{
    public static async Task SeedDataAsync(AppDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            List<IdentityRole> roles =
            [
                new() { Name = UserRoles.User } ,
                new() { Name = UserRoles.Admin }
            ];

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        if (!userManager.Users.Any())
        {
            var admin = new User
            {
                Id = "admin-id",
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                Email = "admin@test.com"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");

            await userManager.AddToRoleAsync(admin, UserRoles.Admin);

            List<User> users =
            [
                new()
                {
                    Id = "jane-id",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane@test.com",
                    UserName = "jane"
                },

                new()
                {
                    Id = "emily-id",
                    FirstName = "Emily",
                    LastName = "brown",
                    Email = "emily@test.com",
                    UserName = "emily"
                },

                new()
                {
                    Id = "daniel-id",
                    FirstName = "Daniel",
                    LastName = "Williams",
                    Email = "daniel@test.com",
                    UserName = "daniel"
                }
            ];

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");

                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }

        if (!context.Movies.IgnoreQueryFilters().Any())
        {
            List<Movie> movies =
            [
                new ()
                {
                    Id = "avengers-id",
                    Title = "Avengers: Endgame",
                    Genre = "Action, Adventure, Sci-Fi",
                    Description = "The Avengers assemble once more to undo the damage caused by Thanos in this epic conclusion to the Infinity Saga.",
                    ReleaseDate = new DateOnly(2019, 4, 26),
                    Director = "Anthony and Joe Russo",
                    Language = "English",
                    Duration = "3 hours, 2 minutes",
                    Rating = 8.4m
                },

                new ()
                {
                    Id = "ironman-id",
                    Title = "Iron Man",
                    Genre = "Action, Adventure, Sci-Fi",
                    Description = "A wealthy industrialist is forced to build a high-tech suit of armor to escape captivity, only to discover it has greater implications.",
                    ReleaseDate = new DateOnly(2008, 5, 2),
                    Director = "Jon Favreau",
                    Language = "English",
                    Duration = "2 hours, 6 minutes",
                    Rating = 7.9m
                },

                new ()
                {
                    Id = "spiderman-id",
                    Title = "Spider-Man: No Way Home",
                    Genre = "Action, Adventure, Sci-Fi",
                    Description = "Spider-Man seeks the help of Doctor Strange to make his identity a secret once again, but things go wrong, resulting in alternate realities.",
                    ReleaseDate = new DateOnly(2021, 12, 17),
                    Director = "Jon Watts",
                    Language = "English",
                    Duration = "2 hours, 28 minutes",
                    Rating = 8.3m
                },

                new ()
                {
                    Id = "guradians-id",
                    Title = "Guardians of the Galaxy",
                    Genre = "Action, Adventure, Comedy",
                    Description = "A group of misfits in space must band together to stop a villain from taking over the galaxy.",
                    ReleaseDate = new DateOnly(2014, 8, 1),
                    Director = "James Gunn",
                    Language = "English",
                    Duration = "2 hours, 1 minute",
                    Rating = 8.0m
                },

                new ()
                {
                    Id = "blackPanther-id",
                    Title = "Black Panther",
                    Genre = "Action, Adventure, Sci-Fi",
                    Description = "T'Challa returns to his homeland of Wakanda to take the throne after the death of his father, but a new challenge arises.",
                    ReleaseDate = new DateOnly(2018, 2, 16),
                    Director = "Ryan Coogler",
                    Language = "English",
                    Duration = "2 hours, 14 minutes",
                    Rating = 7.3m
                },

                new ()
                {
                    Id = "inception-id",
                    Title = "Inception",
                    Genre = "Action, Adventure, Sci-Fi, Thriller",
                    Description = "A thief who enters the dreams of others to steal secrets finds himself tasked with planting an idea into the mind of a CEO.",
                    ReleaseDate = new DateOnly(2010, 7, 16),
                    Director = "Christopher Nolan",
                    Language = "English",
                    Duration = "2 hours, 28 minutes",
                    Rating = 8.8m
                },

                new ()
                {
                    Id = "interstellar-id",
                    Title = "Interstellar",
                    Genre = "Adventure, Drama, Sci-Fi",
                    Description = "A team of astronauts undertakes a dangerous mission through a wormhole near Saturn in search of a new habitable planet for humanity.",
                    ReleaseDate = new DateOnly(2014, 11, 7),
                    Director = "Christopher Nolan",
                    Language = "English",
                    Duration = "2 hours, 49 minutes",
                    Rating = 8.6m
                },

                new ()
                {
                    Id = "theMartian-id",
                    Title = "The Martian",
                    Genre = "Adventure, Drama, Sci-Fi",
                    Description = "An astronaut stranded on Mars must find a way to survive while NASA works to bring him back home.",
                    ReleaseDate = new DateOnly(2015, 10, 2),
                    Director = "Ridley Scott",
                    Language = "English",
                    Duration = "2 hours, 24 minutes",
                    Rating = 8.0m
                },

                new ()
                {
                    Id = "forrestGump-id",
                    Title = "Forrest Gump",
                    Genre = "Drama, Romance",
                    Description = "The life journey of a man with a low IQ, who unwittingly becomes part of many significant historical events.",
                    ReleaseDate = new DateOnly(1994, 7, 6),
                    Director = "Robert Zemeckis",
                    Language = "English",
                    Duration = "2 hours, 22 minutes",
                    Rating = 8.8m
                },

                new ()
                {
                    Id = "shawshankRedemption-id",
                    Title = "The Shawshank Redemption",
                    Genre = "Drama",
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    ReleaseDate = new DateOnly(1994, 10, 14),
                    Director = "Frank Darabont",
                    Language = "English",
                    Duration = "2 hours, 22 minutes",
                    Rating = 9.3m
                }

            ];

            context.Movies.AddRange(movies);

            await context.SaveChangesAsync();
        }
    }
}
