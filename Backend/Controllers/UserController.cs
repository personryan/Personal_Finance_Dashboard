using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;
using Backend.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    using BCrypt = BCrypt.Net.BCrypt;
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            Console.WriteLine($"Registration with: Email = {user.Email}, Password = {user.PasswordHash}");
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                Console.WriteLine("Missing Email or Password");
                return BadRequest(new { message = "Email and password are required." });
            }

            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                Console.WriteLine("Invalid Email");
                return BadRequest(new { message = "Email is already in use." });
            }

            // Hash the password before saving
            user.PasswordHash = BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }

        // Login a user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            Console.WriteLine($"Login attempt: Email = {request.Email}");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return Unauthorized(new { message = "Invalid credentials." });
            }
            Console.WriteLine($"User: {user.Email}, Password: {user.PasswordHash}");
            if (!BCrypt.Verify(request.Password, user.PasswordHash))
            {
                Console.WriteLine("Invalid password.");
                return Unauthorized(new { message = "Invalid credentials " });
            }

            // Generate JWT token
            var token = JwtHelper.GenerateJwtToken(user, _config);

            // Generate Refresh Token
            var refreshToken = JwtHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { token, refreshToken, message = "Login successful!" });
        }

        // JWT Token Expires
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Invalid or Expired Refresh Token" });
            }

            // Generate new JWT
            var newToken = JwtHelper.GenerateJwtToken(user, _config);

            // Generate new refresh Token
            var newRefreshToken = JwtHelper.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { token = newToken, refreshToken = newRefreshToken });

        }
        // Get transactions for a user testing need to include email later
        [HttpGet("transactions")]
        [Authorize]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = new List<object> // Replace this with your actual DB query
            {
                new { Id = 1, Amount = 100, Type = "Deposit" },
                new { Id = 2, Amount = -50, Type = "Withdrawal" }
            };

            return Ok(transactions);
        }

    }
}
