using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<Loan?> GetLoanByIdAsync(int id);
        Task<Loan> CreateLoanAsync(Loan loan);
        Task<Loan> UpdateLoanAsync(Loan loan);
        Task<bool> DeleteLoanAsync(int id);
        Task<IEnumerable<Loan>> GetLoansByUserAsync(string userId);
        Task<IEnumerable<Loan>> GetActiveLoansByUserAsync(string userId);
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();
        Task<bool> ReturnBookAsync(int loanId);
        Task<decimal> CalculateLateFeeAsync(int loanId);
    }
}

