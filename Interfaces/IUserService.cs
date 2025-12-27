using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(string id);
        Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string searchTerm);
    }
}

