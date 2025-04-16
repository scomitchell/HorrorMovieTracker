using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;
using HorrorMovieBackend.Services;
using HorrorMovieBackend.Models;

namespace HorrorMovieBackend.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            // Set environment variable for test scope
            Environment.SetEnvironmentVariable("JWT_SECRET_KEY", "this_is_a_much_longer_test_secret_key_32_bytes");

            _authService = new AuthService();
        }

        [Fact]
        public void GenerateToken_ShouldReturn_ValidToken()
        {
            string testusername = "testuser";

            var user = new User
            {
                Id = 1,
                Username = testusername
            };

            string token = _authService.GenerateToken(user);

            Assert.False(string.IsNullOrEmpty(token));

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("this_is_a_much_longer_test_secret_key_32_bytes");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            handler.ValidateToken(token, tokenValidationParameters, out var ValidatedToken);

            Assert.NotNull(ValidatedToken);
            Assert.IsType<JwtSecurityToken>(ValidatedToken);
        }
    }
}