using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<Publisher?> GetPublisherByIdAsync(int id);
        Task<Publisher> CreatePublisherAsync(Publisher publisher);
        Task<Publisher> UpdatePublisherAsync(Publisher publisher);
        Task<bool> DeletePublisherAsync(int id);
    }
}

