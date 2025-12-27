using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        public string? Notes { get; set; }

        // Navigation properties
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
    }

    public enum ReservationStatus
    {
        Pending,
        Fulfilled,
        Cancelled,
        Expired
    }
}

