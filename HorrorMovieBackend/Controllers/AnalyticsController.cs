using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Data;
using HorrorMovieBackend.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// Provides API endpoints for user analytics
namespace HorrorMovieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnalyticsController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Produces summary of user activity
        [HttpGet("summary")]
        public async Task<IActionResult> GetUserSummary()
        {
            // Extract user ID from JWT token
            var userIdStr = GetUserId();
            if (userIdStr == null) return Unauthorized();

            // Parse User ID as an Integer
            if (!int.TryParse(userIdStr, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            // Calculate user statistics

            // Count number of movies added to user's personal list
            var watchedCount = await _context.UserMovies.CountAsync(um => um.UserId == userId);

            // Count the number of reviews written by the user
            var reviewCount = await _context.Reviews.CountAsync(r => r.UserId == userId);

            // Calculate the average rating given by the user
            var ratings = await _context.Reviews
                .Where(r => r.UserId == userId) // Filter reviews to authenticated user
                .Select(r => r.Rating) // Select the rating value from reviews
                .ToListAsync(); // Create a list of ratings from user

            var averageRating = ratings.Any() ? ratings.Average() : 0;

            // Calculate the user's most-watched subgenre
            var topSubgenre = await _context.UserMovies
                .Where(um => um.UserId == userId)
                .Include(um => um.Movie) // Include related movie details
                .GroupBy(um => um.Movie.Subgenre) // Group by subgenre
                .OrderByDescending(g => g.Count()) // Order by count of movies in each subgenre
                .Select(g => g.Key) // Select the subgenre
                .FirstOrDefaultAsync(); // Return the top result or null

            return Ok(new
            {
                watchedCount,
                reviewCount,
                averageRating = Math.Round(averageRating, 2),
                topSubgenre = topSubgenre ?? "N/A"
            });
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}