using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Loans)
                .Include(b => b.Reservations)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            book.CreatedAt = DateTime.UtcNow;
            book.AvailableCopies = book.TotalCopies;
            
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            book.UpdatedAt = DateTime.UtcNow;
            
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Loans)
                .Include(b => b.Reservations)
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.BookId == id);
                
            if (book == null)
                return false;

            // Check if book has active loans or reservations
            if (book.Loans.Any(l => l.Status == LoanStatus.Active))
            {
                throw new InvalidOperationException("Cannot delete book with active loans. Please wait for all loans to be returned.");
            }

            if (book.Reservations.Any(r => r.Status == ReservationStatus.Pending || r.Status == ReservationStatus.Fulfilled))
            {
                throw new InvalidOperationException("Cannot delete book with active reservations. Please cancel all reservations first.");
            }

            // Delete related records first
            if (book.BookAuthors.Any())
            {
                _context.BookAuthors.RemoveRange(book.BookAuthors);
            }

            // Delete the book
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Where(b => b.Title.Contains(searchTerm) || 
                           b.ISBN.Contains(searchTerm) ||
                           b.Description!.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == authorId))
                .ToListAsync();
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            return book != null && book.AvailableCopies > 0;
        }
    }
}

