using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Configuration;
using HorrorMovieBackend.Services;
using HorrorMovieBackend.Models;
using HorrorMovieBackend.Data;
using HorrorMovieBackend.Controllers;

namespace HorrorMovieBackend.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly ApplicationDbContext _dbContext;
        private readonly AuthService _authService;

        public AuthControllerTests()
        {
            // Set up database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApplicationDbContext(options);

            // Mock IConfiguration
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(config => config["Jwt:Key"]).Returns("this_is_a_much_longer_test_secret_key_32_bytes");
            _authService = new AuthService(mockConfig.Object);

            SeedDatabase();

            _controller = new AuthController(_dbContext, _authService);
        }

        private void Dispose()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users);
            _dbContext.SaveChanges();
        }

        private void SeedDatabase()
        {
            if (!_dbContext.Users.Any())
            {
                _dbContext.Add(new User
                {
                    Id = 1,
                    Username = "testuser1",
                    Password = "testuserpw1"
                });

                _dbContext.Add(new User
                {
                    Id = 2,
                    Username = "testUser2",
                    Password = "testuserpw2"
                });

                _dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task RegisterUser_RegistersUser()
        {
            var newUser = new User
            {
                Id = 3,
                Username = "newUser",
                Password = "newuserpw"
            };

            var result = await _controller.Register(newUser);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User registered successfully", okResult.Value);

            var registeredUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == "newUser");
            Assert.NotNull(registeredUser);
            Assert.Equal("newUser", registeredUser.Username);
        }

        [Fact]
        public async Task LoginUser_ValidCredentials_GeneratesToken()
        {
            var loginUser = new User
            {
                Id = 1,
                Username = "testuser1",
                Password = "testuserpw1"
            };

            var result = await _controller.Login(loginUser);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.NotNull(response);
            Assert.NotNull(response.Token);
            Assert.IsType<string>(response.Token);
        }
    }
}