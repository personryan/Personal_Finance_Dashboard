using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            //if (_context.Users.Any(u => u.Email == user.Email))
            //{
            //    return BadRequest("Email is already in use.");
            //}

            //user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // Hash password
            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();
            return Ok("User registered successfully");
        }

        // Login a user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            //var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            //if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, dbUser.PasswordHash))
            //{
            //    return Unauthorized("Invalid credentials");


            //return Ok(new { message = "Login successful", userId = dbUser.Id });
            return Ok(1);
        }

        // Get transactions for a user
        [HttpGet("{userId}/transactions")]
        public async Task<IActionResult> GetUserTransactions(int userId)
        {
            //var transactions = await _context.Transactions
            //    .Where(t => t.UserId == userId)
            //    .ToListAsync();

            return Ok(1);
        }
    }
}
