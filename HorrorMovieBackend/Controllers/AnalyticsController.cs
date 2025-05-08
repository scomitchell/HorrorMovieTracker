using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Data;
using HorrorMovieBackend.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpGet("summary")]
        public async Task<IActionResult> GetUserSummary()
        {
            var userIdStr = GetUserId();
            if (userIdStr == null) return Unauthorized();

            if (!int.TryParse(userIdStr, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var watchedCount = await _context.UserMovies.CountAsync(um => um.UserId == userId);
            var reviewCount = await _context.Reviews.CountAsync(r => r.UserId == userId);
            var ratings = await _context.Reviews
                .Where(r => r.UserId == userId)
                .Select(r => r.Rating)
                .ToListAsync();

            var averageRating = ratings.Any() ? ratings.Average() : 0;

            var topSubgenre = await _context.UserMovies
                .Where(um => um.UserId == userId)
                .Include(um => um.Movie)
                .GroupBy(um => um.Movie.Subgenre)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

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