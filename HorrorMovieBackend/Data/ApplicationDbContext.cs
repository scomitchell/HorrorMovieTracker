using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Models;

namespace HorrorMovieBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}