using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly ApplicationDbContext _context;

        public PublisherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            return await _context.Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.PublisherId == id);
        }

        public async Task<Publisher> CreatePublisherAsync(Publisher publisher)
        {
            publisher.CreatedAt = DateTime.UtcNow;
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher> UpdatePublisherAsync(Publisher publisher)
        {
            _context.Entry(publisher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                return false;

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

