using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorMovieBackend.Controllers;
using HorrorMovieBackend.Models;
using HorrorMovieBackend.Data;

namespace HorrorMovieBackend.Tests.Controllers
{
    public class UserMoviesControllerTests
    {
        private readonly UserMoviesController _controller;
        private readonly ApplicationDbContext _dbContext;

        public UserMoviesControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);

            SeedDatabase();

            _controller = new UserMoviesController(_dbContext);

            // Simulate authenticated user with ID = 1
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        private void SeedDatabase()
        {
            if (!_dbContext.Movies.Any())
            {
                var movie1 = new Movie
                {
                    Id = 1, // Add explicit ID to match UserMovie
                    Title = "The Shining",
                    Subgenre = "Horror",
                    ReleaseDate = new DateTime(1980, 5, 23),
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BNmM5ZThhY2ItOGRjOS00NzZiLWEwYTItNDgyMjFkOTgxMmRiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Description = "Jack Torrance (Jack Nicholson) becomes winter caretaker..."
                };

                var movie2 = new Movie
                {
                    Id = 2,
                    Title = "Scream",
                    Subgenre = "Slasher",
                    ReleaseDate = new DateTime(1996, 12, 19),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/86/Scream_%281996_film%29_poster.jpg",
                    Description = "Wes Craven re-invented the slasher-horror genre..."
                };

                _dbContext.Movies.AddRange(movie1, movie2);
                _dbContext.SaveChanges();

                _dbContext.UserMovies.Add(new UserMovie
                {
                    UserId = 1,
                    MovieId = 1 
                });

                _dbContext.SaveChanges();
            }
        }


        [Fact]
        public async Task GetMovies_ReturnsUserMovies()
        {
            var result = await _controller.GetMovies();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userMovies = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);

            Assert.Single(userMovies);
        }
    }
}