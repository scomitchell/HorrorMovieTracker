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
                    ReleaseDate = new DateTime(1980, 5, 23),
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BNmM5ZThhY2ItOGRjOS00NzZiLWEwYTItNDgyMjFkOTgxMmRiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg"
                },
                new Movie
                {
                    Id = 2,
                    Title = "Scream",
                    Subgenre = "Slasher",
                    ReleaseDate = new DateTime(1996, 12, 19),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/86/Scream_%281996_film%29_poster.jpg"
                },
                new Movie
                {
                    Id = 3,
                    Title = "A Nightmare on Elm Street",
                    Subgenre = "Horror",
                    ReleaseDate = new DateTime(1984, 11, 9),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/f/fa/A_Nightmare_on_Elm_Street_%281984%29_theatrical_poster.jpg"
                }
            );

            context.SaveChanges();  // Save the changes to the database
        }
    }
}