namespace HorrorMovieBackend.Models
{
    public class UserMovie
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        public Movie Movie { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}