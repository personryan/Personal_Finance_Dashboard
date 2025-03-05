using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
	public class Transaction
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; } // Description of transaction

		[Required]
		public decimal Amount { get; set; } // Amount spent/received

		[Required]
		public DateTime TransactionDate { get; set; } // When the transaction happened

		[Required]
		public TransactionCategory Category { get; set; } // Enum instead of string

		[Required]
		public bool IsExpense { get; set; } // true if it's an expense, false if income

		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
