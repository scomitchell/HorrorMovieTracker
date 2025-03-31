using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorMovieBackend.Models;
using HorrorMovieBackend.Data;

namespace HorrorMovieBackend.Services
{
    public class MovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new movie
        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        // Get a movie by its ID
        public async Task<Movie> GetMovieByID(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        // Update an existing movie
        public async Task<Movie> updateMovieAsync(int id, Movie updatedMovie)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return null;
            }

            movie.Title = updatedMovie.Title;
            movie.Subgenre = updatedMovie.Subgenre;
            movie.ReleaseDate = updatedMovie.ReleaseDate;

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        // Delete a movie
        public async Task<bool> deleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
