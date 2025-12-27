using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReservationId == id);
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            reservation.ReservationDate = DateTime.UtcNow;
            reservation.ExpiryDate = DateTime.UtcNow.AddDays(7); // Reservation valid for 7 days
            reservation.Status = ReservationStatus.Pending;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId)
        {
            return await _context.Reservations
                .Include(r => r.Book)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetPendingReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.Status == ReservationStatus.Pending)
                .ToListAsync();
        }

        public async Task<bool> FulfillReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
                return false;

            reservation.Status = ReservationStatus.Fulfilled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
                return false;

            reservation.Status = ReservationStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

