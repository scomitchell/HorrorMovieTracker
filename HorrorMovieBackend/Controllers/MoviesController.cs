using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorrorMovieBackend.Data;
using HorrorMovieBackend.Models;
using HorrorMovieBackend.Services;

namespace HorrorMovieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByID(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, [FromBody] Movie movie)
        {
            var updatedMovie = await _movieService.updateMovieAsync(id, movie);
            if (updatedMovie != null)
            {
                return NotFound();
            }

            return Ok(updatedMovie);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _movieService.deleteMovieAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}