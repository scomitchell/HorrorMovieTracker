using HorrorMovieBackend.Models;
using HorrorMovieBackend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Net.WebSockets;

namespace HorrorMovieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication
    public class UserMoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("my-movies")]
        public async Task<IActionResult> GetMovies()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var userMovies = await _context.UserMovies
                .Where(um => um.UserId == userId)
                .Include(um => um.Movie)
                .Select(um => new
                {
                    um.Movie.Id,
                    um.Movie.Title,
                    um.Movie.Subgenre,
                    um.Movie.ReleaseDate,
                    um.Movie.ImageUrl,
                })
                .ToListAsync();
            
            return Ok(userMovies);
        }

    
        [HttpPost]
        public async Task<IActionResult> AddMovieToUserList([FromBody] Movie movie)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Check if movie exists in the database and add it if it doesn't
            var existingMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Title == movie.Title);
            if (existingMovie == null)
            {
                existingMovie = new Movie
                {
                    Title = movie.Title,
                    Subgenre = movie.Subgenre,
                    ReleaseDate = movie.ReleaseDate,
                    ImageUrl = movie.ImageUrl
                };

                _context.Movies.Add(existingMovie);
                await _context.SaveChangesAsync();
            }

            // Check if the User has the movie in their list
            var userMovieExists = await _context.UserMovies
                .AnyAsync(um => um.UserId == userId && um.Movie.Id == existingMovie.Id);

            if (userMovieExists)
            {
                return BadRequest("Movie already exists in your list");
            }

            var userMovie = new UserMovie
            {
                MovieId = existingMovie.Id,
                UserId = userId
            };

            _context.UserMovies.Add(userMovie);
            await _context.SaveChangesAsync();

            return Ok("Movie added to your list");
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> RemoveMovieFromUserList(int movieId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var userMovie = await _context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (userMovie == null)
            {
                return NotFound("Movie does not exist in User list");
            }

            _context.UserMovies.Remove(userMovie);
            await _context.SaveChangesAsync();

            return Ok("Movie removed from your list");
        }
    }
}