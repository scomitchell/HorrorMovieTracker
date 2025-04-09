using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HorrorMovieBackend.Services;
using HorrorMovieBackend.Models;
using HorrorMovieBackend.Data;

namespace HorrorMovieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;

        public AuthController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            // Check if a user with username already exists
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                return BadRequest("Username is already taken");
            }

            // Hash user password
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _authService.GenerateToken(user);
            var response = new TokenResponse { Token = token };
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            // Retrieve user from database given provided Username
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            // Check if user exists and password matches the stored hash
            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            // Generate a JWT for the authenticated user
            var token = _authService.GenerateToken(existingUser);

            // Return the token wrapped in a TokenResponse
            var response = new TokenResponse { Token = token };
            return Ok(response); // Return TokenResponse
        }
    }
}