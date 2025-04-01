using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Models;

namespace HorrorMovieBackend.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            // Check if the movies table already has data
            if (context.Movies.Any())
            {
                return; // DB has been seeded
            }

            // Add initial movies to the database
            context.Movies.AddRange(
                new Movie
                {
                    Id = 1,
                    Title = "The Shining",
                    Subgenre = "Horror",
                    ReleaseDate = new DateTime(1980, 5, 23)
                },
                new Movie
                {
                    Id = 2,
                    Title = "Scream",
                    Subgenre = "Slasher",
                    ReleaseDate = new DateTime(1996, 12, 19)
                },
                new Movie
                {
                    Id = 3,
                    Title = "A Nightmare on Elm Street",
                    Subgenre = "Horror",
                    ReleaseDate = new DateTime(1984, 11, 9)
                }
            );

            context.SaveChanges();  // Save the changes to the database
        }
    }
}