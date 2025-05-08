using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Data;
using HorrorMovieBackend.Models;

namespace HorrorMovieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnalyticsController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        HttpGet("summary")]
        public async Task<IActionResult> GetUserSummary()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var watchedCount = await _context.UserMovies.CountAsync(um => um.UserId == userId);
            var reviewCount = await _context.Reviews.CountAsync(r => r.UserId == userId);
            var averageRating = await _context.Reviews
                .Where(r => r.UserId == userId)
                .Select(r => r.Rating)
                .DefaultIfEmpty(0)
                .AverageAsync();
        }
    }
}