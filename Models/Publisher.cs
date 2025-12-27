using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Publisher name is required")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Address { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Url]
        public string? Website { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

