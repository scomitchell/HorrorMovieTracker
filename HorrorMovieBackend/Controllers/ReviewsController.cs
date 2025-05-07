using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HorrorMovieBackend.Data;
using HorrorMovieBackend.Models;
using System.Security.Claims;

namespace HorrorMovieBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class ReviewsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ReviewsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateReview([FromBody] ReviewRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var review = new Review
			{
				Content = request.Content,
				Rating = request.Rating,
				MovieId = request.MovieId,
				UserId = userId
			};

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "review created" });
        }

		[HttpGet("movie/{movieId}")]
		public async Task<IActionResult> GetReviewsForMovie(int movieId)
		{
			var reviews = await _context.Reviews
				.Where(r => r.MovieId == movieId)
				.Select(r => new
				{
					r.Id,
					r.Content,
					r.Rating
				})
				.ToListAsync();

			return Ok(reviews);
		}

		[HttpGet("movie/{movieId}/rating")]
		public async Task<IActionResult> GetAverageRatingForMovie(int movieId)
		{
			var reviews = await _context.Reviews
				.Where(r => r.MovieId == movieId)
				.ToListAsync();

			if (!reviews.Any())
			{
				return Ok(0);
			}

			var averageRating = reviews.Average(r => r.Rating);

			return Ok(averageRating);
        }
	}
}