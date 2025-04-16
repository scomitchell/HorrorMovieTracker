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
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BNmM5ZThhY2ItOGRjOS00NzZiLWEwYTItNDgyMjFkOTgxMmRiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Description = "Jack Torrance (Jack Nicholson) becomes winter caretaker at the isolated " +
                    "Overlook Hotel in Colorado, hoping to cure his writer's block. He settles in along with " +
                    "his wife, Wendy (Shelley Duvall), and his son, Danny (Danny Lloyd), who is plagued by psychic " +
                    "premonitions. As Jack's writing goes nowhere and Danny's visions become more disturbing, " +
                    "Jack discovers the hotel's dark secrets and begins to unravel into a homicidal maniac " +
                    "hell-bent on terrorizing his family."
                },
                new Movie
                {
                    Id = 2,
                    Title = "Scream",
                    Subgenre = "Slasher",
                    ReleaseDate = new DateTime(1996, 12, 19),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/86/Scream_%281996_film%29_poster.jpg",
                    Description = "Wes Craven re-invented and revitalised the slasher-horror genre with this modern " +
                    "horror classic, which manages to be funny, clever and scary, as a fright-masked knife maniac stalks " +
                    "high-school students in middle-class suburbia. Craven is happy to provide both tension and self-parody " +
                    "as the body count mounts - but the victims aren't always the ones you'd expect."
                },
                new Movie
                {
                    Id = 3,
                    Title = "A Nightmare on Elm Street",
                    Subgenre = "Horror",
                    ReleaseDate = new DateTime(1984, 11, 9),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/f/fa/A_Nightmare_on_Elm_Street_%281984%29_theatrical_poster.jpg",
                    Description = "In Wes Craven's classic slasher film, several Midwestern teenagers fall prey to Freddy " +
                    "Krueger (Robert Englund), a disfigured midnight mangler who preys on the teenagers in their dreams -- " +
                    "which, in turn, kills them in reality. After investigating the phenomenon, Nancy (Heather Langenkamp) " +
                    "begins to suspect that a dark secret kept by her and her friends' parents may be the key to unraveling " +
                    "the mystery, but can Nancy and her boyfriend Glen (Johnny Depp) solve the puzzle before it's too late?"
                }
            );

            context.SaveChanges();  // Save the changes to the database
        }
    }
}