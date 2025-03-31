using System.ComponentModel.DataAnnotations;

namespace HorrorMovieBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
    }
}