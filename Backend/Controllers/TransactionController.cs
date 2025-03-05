using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        // Get all transactions
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            //var transactions = await _context.Transactions.ToListAsync();
            //return Ok(transactions);
            return Ok(1);
        }

        // Get transaction by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            //var transaction = await _context.Transactions.FindAsync(id);
            //if (transaction == null) return NotFound("Transaction not found.");
            //return Ok(transaction);
            return Ok(1);
        }

        // Add a new transaction
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            //_context.Transactions.Add(transaction);
            //await _context.SaveChangesAsync();
            return Ok("Transaction added successfully.");
        }

        // Delete a transaction
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            //var transaction = await _context.Transactions.FindAsync(id);
            //if (transaction == null) return NotFound("Transaction not found.");

            //_context.Transactions.Remove(transaction);
            //await _context.SaveChangesAsync();
            return Ok("Transaction deleted successfully.");
        }
    }
}
