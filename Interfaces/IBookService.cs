using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
        Task<bool> IsBookAvailableAsync(int bookId);
    }
}

