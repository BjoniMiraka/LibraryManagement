using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public int PublishedYear { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total copies must be at least 1")]
        public int TotalCopies { get; set; }

        [Required]
        public int AvailableCopies { get; set; }

        public string? CoverImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Foreign Keys
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int PublisherId { get; set; }

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;

        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; } = null!;

        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

