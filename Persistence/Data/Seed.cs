using Domain.Entities;

namespace Persistence.Data;

public class Seed
{
    public static async Task SeedDataAsync(AppDbContext context)
    {
        if (!context.Movies.Any())
        {
            List<Movie> movies =
            [
                new ()
                {
                    Id = "avengersId",
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
                    Id = "ironmanId",
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
                    Id = "spidermanId",
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
                    Id = "guradiansId",
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
                    Id = "blackPantherId",
                    Title = "Black Panther",
                    Genre = "Action, Adventure, Sci-Fi",
                    Description = "T'Challa returns to his homeland of Wakanda to take the throne after the death of his father, but a new challenge arises.",
                    ReleaseDate = new DateOnly(2018, 2, 16),
                    Director = "Ryan Coogler",
                    Language = "English",
                    Duration = "2 hours, 14 minutes",
                    Rating = 7.3m
                }

            ];

            context.Movies.AddRange(movies);

            await context.SaveChangesAsync();
        }
    }
}
