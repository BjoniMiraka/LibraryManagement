using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> CreateAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);
        Task<IEnumerable<Author>> SearchAuthorsAsync(string searchTerm);
    }
}

