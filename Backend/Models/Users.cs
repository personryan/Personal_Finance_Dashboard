using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Store the hashed password

        public string? RefreshToken { get; set; } // Store refresh token for authentication
        public DateTime? RefreshTokenExpiry { get; set; } // Expiry time for refresh token

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relationship: A user can have multiple transactions
        public List<Transaction>? Transactions { get; set; } = new List<Transaction>();
    }
}
