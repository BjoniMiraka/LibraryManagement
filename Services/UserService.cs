using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _context.Users
                .Include(u => u.Loans)
                .Include(u => u.Reservations)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string searchTerm)
        {
            return await _context.Users
                .Where(u => u.FirstName.Contains(searchTerm) || 
                           u.LastName.Contains(searchTerm) ||
                           u.Email!.Contains(searchTerm))
                .ToListAsync();
        }
    }
}

