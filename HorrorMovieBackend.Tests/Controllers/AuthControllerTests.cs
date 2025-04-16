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

            Environment.SetEnvironmentVariable("JWT_SECRET_KEY", "this_is_a_much_longer_test_secret_key_32_bytes");
            _authService = new AuthService();

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
                    Password = BCrypt.Net.BCrypt.HashPassword("testuserpw1")
                });

                _dbContext.Add(new User
                {
                    Id = 2,
                    Username = "testUser2",
                    Password = BCrypt.Net.BCrypt.HashPassword("testuserpw2")
                });

                _dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task RegisterUser_RegistersUser()
        {
            var newUser = new User
            {
                Username = "newuser",
                Password = "securepassword"
            };

            var result = await _controller.Register(newUser);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var tokenResponse = Assert.IsType<TokenResponse>(okResult.Value);

            Assert.False(string.IsNullOrEmpty(tokenResponse.Token));
        }


        [Fact]
        public async Task LoginUser_InvalidPassword_ReturnsUnauthorized()
        {
            // Create user with invalid credentials
            var loginUser = new User
            {
                Id = 1,
                Username = "testuser1",
                Password = "testuserpw2"
            };

            // Call the login method
            var result = await _controller.Login(loginUser);

            // Verify unauthorized repsonse
            var rejectedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid username or password", rejectedResult.Value);
        }

        [Fact]
        public async Task LoginUser_ValidCredentials_GeneratesToken()
        {
            // Create a user with valid credentials
            var loginUser = new User
            {
                Id = 1,
                Username = "testuser1",
                Password = "testuserpw1" // This should match the password in SeedDatabase
            };

            // Call the login method
            var result = await _controller.Login(loginUser);

            // Check that the result is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Ensure the result is a TokenResponse type
            var response = Assert.IsType<TokenResponse>(okResult.Value);

            Assert.NotNull(response);
            Assert.NotNull(response.Token);
            Assert.IsType<string>(response.Token);  // Ensure it's a string
        }
    }
}