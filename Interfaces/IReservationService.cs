using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task<Reservation> UpdateReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId);
        Task<IEnumerable<Reservation>> GetPendingReservationsAsync();
        Task<bool> FulfillReservationAsync(int reservationId);
        Task<bool> CancelReservationAsync(int reservationId);
    }
}

