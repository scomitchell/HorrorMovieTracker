using System.ComponentModel.DataAnnotations;

namespace HorrorMovieBackend.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}