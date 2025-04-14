using System.ComponentModel.DataAnnotations;

namespace HorrorMovieBackend.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(100)]
        public string Subgenre { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public List<UserMovie> UserMovies { get; set; } = new();

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }
    }
}