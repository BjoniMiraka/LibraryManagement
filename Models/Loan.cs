using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public LoanStatus Status { get; set; } = LoanStatus.Active;

        [Column(TypeName = "decimal(18,2)")]
        public decimal? LateFee { get; set; }

        public string? Notes { get; set; }

        // Navigation properties
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        // Computed property
        public bool IsOverdue => Status == LoanStatus.Active && DateTime.UtcNow > DueDate;
    }

    public enum LoanStatus
    {
        Active,
        Returned,
        Overdue,
        Lost
    }
}

