using LibraryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200)]
        [Display(Name = "Book Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(13)]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Published year is required")]
        [Range(1900, 2026, ErrorMessage = "Please enter a valid year")]
        [Display(Name = "Published Year")]
        public int PublishedYear { get; set; }

        [StringLength(1000)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Total copies is required")]
        [Range(1, 1000, ErrorMessage = "Total copies must be between 1 and 1000")]
        [Display(Name = "Total Copies")]
        public int TotalCopies { get; set; }

        [Display(Name = "Cover Image URL")]
        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }

        [Display(Name = "Authors")]
        public List<int> SelectedAuthorIds { get; set; } = new List<int>();

        // For display purposes
        public Category? Category { get; set; }
        public Publisher? Publisher { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}

