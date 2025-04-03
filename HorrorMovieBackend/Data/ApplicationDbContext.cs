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
        public DbSet<User> Users { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key to prevent duplciate user-movie entriee
            modelBuilder.Entity<UserMovie>()
                .HasKey(um => new { um.UserId, um.MovieId });

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.User)
                .WithMany(u => u.UserMovies)
                .HasForeignKey(um => um.UserId);

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.Movie)
                .WithMany(u => u.UserMovies)
                .HasForeignKey(um => um.MovieId);
        }
    }
}