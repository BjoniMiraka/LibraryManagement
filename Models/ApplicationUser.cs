using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

