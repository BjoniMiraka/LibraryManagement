using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync(a => a.AuthorId == id);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            author.CreatedAt = DateTime.UtcNow;
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Author>> SearchAuthorsAsync(string searchTerm)
        {
            return await _context.Authors
                .Where(a => a.FirstName.Contains(searchTerm) || 
                           a.LastName.Contains(searchTerm) ||
                           a.Country!.Contains(searchTerm))
                .ToListAsync();
        }
    }
}

