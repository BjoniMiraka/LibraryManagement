using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Country { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

        // Computed property
        public string FullName => $"{FirstName} {LastName}";
    }
}

