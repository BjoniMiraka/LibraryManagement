using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;
        private const decimal LateFeePerDay = 1.00m;

        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .ToListAsync();
        }

        public async Task<Loan?> GetLoanByIdAsync(int id)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.LoanId == id);
        }

        public async Task<Loan> CreateLoanAsync(Loan loan)
        {
            loan.LoanDate = DateTime.UtcNow;
            loan.DueDate = DateTime.UtcNow.AddDays(14); // 2 weeks loan period
            loan.Status = LoanStatus.Active;

            var book = await _context.Books.FindAsync(loan.BookId);
            if (book != null && book.AvailableCopies > 0)
            {
                book.AvailableCopies--;
                _context.Loans.Add(loan);
                await _context.SaveChangesAsync();
                return loan;
            }

            throw new InvalidOperationException("Book is not available for loan");
        }

        public async Task<Loan> UpdateLoanAsync(Loan loan)
        {
            _context.Entry(loan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<bool> DeleteLoanAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
                return false;

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserAsync(string userId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetActiveLoansByUserAsync(string userId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.UserId == userId && l.Status == LoanStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(l => l.Status == LoanStatus.Active && l.DueDate < DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<bool> ReturnBookAsync(int loanId)
        {
            var loan = await _context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.LoanId == loanId);

            if (loan == null)
                return false;

            loan.ReturnDate = DateTime.UtcNow;
            loan.Status = LoanStatus.Returned;

            if (loan.ReturnDate > loan.DueDate)
            {
                loan.LateFee = await CalculateLateFeeAsync(loanId);
            }

            loan.Book.AvailableCopies++;
            
            // Check if there are pending reservations for this book
            var pendingReservation = await _context.Reservations
                .Where(r => r.BookId == loan.BookId && r.Status == ReservationStatus.Pending)
                .OrderBy(r => r.ReservationDate) // First come, first served
                .FirstOrDefaultAsync();
            
            if (pendingReservation != null)
            {
                // Fulfill the oldest reservation
                pendingReservation.Status = ReservationStatus.Fulfilled;
                pendingReservation.ExpiryDate = DateTime.UtcNow.AddDays(7); // 7 days to pick up
            }
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> CalculateLateFeeAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null || loan.ReturnDate == null)
                return 0;

            var daysLate = (loan.ReturnDate.Value - loan.DueDate).Days;
            return daysLate > 0 ? daysLate * LateFeePerDay : 0;
        }
    }
}

