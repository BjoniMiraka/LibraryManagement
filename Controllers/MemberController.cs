using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = "Member,Librarian,Admin")]
    public class MemberController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILoanService _loanService;
        private readonly IReservationService _reservationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberController(
            IBookService bookService,
            ILoanService loanService,
            IReservationService reservationService,
            UserManager<ApplicationUser> userManager)
        {
            _bookService = bookService;
            _loanService = loanService;
            _reservationService = reservationService;
            _userManager = userManager;
        }

        // GET: Member/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var loans = await _loanService.GetLoansByUserAsync(user.Id);
            var reservations = await _reservationService.GetReservationsByUserAsync(user.Id);

            ViewBag.ActiveLoans = loans.Count(l => l.Status == LoanStatus.Active);
            ViewBag.OverdueLoans = loans.Count(l => l.IsOverdue && l.Status == LoanStatus.Active);
            ViewBag.ActiveReservations = reservations.Count(r => r.Status == ReservationStatus.Pending);

            return View();
        }

        // GET: Member/MyLoans
        public async Task<IActionResult> MyLoans()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var loans = await _loanService.GetLoansByUserAsync(user.Id);
            return View(loans);
        }

        // GET: Member/MyReservations
        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var reservations = await _reservationService.GetReservationsByUserAsync(user.Id);
            return View(reservations);
        }

        // POST: Member/BorrowBook
        [HttpPost]
        public async Task<JsonResult> BorrowBook(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }

            if (book.AvailableCopies <= 0)
            {
                return Json(new { success = false, message = "No copies available. Would you like to reserve this book?" });
            }

            // Check if user already has this book on loan
            var existingLoans = await _loanService.GetLoansByUserAsync(user.Id);
            if (existingLoans.Any(l => l.BookId == bookId && l.Status == LoanStatus.Active))
            {
                return Json(new { success = false, message = "You already have this book on loan" });
            }

            var loan = new Loan
            {
                BookId = bookId,
                UserId = user.Id,
                LoanDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14), // 14 days loan period
                Status = LoanStatus.Active
            };

            var result = await _loanService.CreateLoanAsync(loan);
            if (result != null)
            {
                return Json(new
                {
                    success = true,
                    message = "Book borrowed successfully!",
                    dueDate = loan.DueDate.ToString("yyyy-MM-dd")
                });
            }

            return Json(new { success = false, message = "Failed to borrow book" });
        }

        // POST: Member/ReserveBook
        [HttpPost]
        public async Task<JsonResult> ReserveBook(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }

            // Check if user already has a reservation for this book
            var existingReservations = await _reservationService.GetReservationsByUserAsync(user.Id);
            if (existingReservations.Any(r => r.BookId == bookId && r.Status == ReservationStatus.Pending))
            {
                return Json(new { success = false, message = "You already have a reservation for this book" });
            }

            var reservation = new Reservation
            {
                BookId = bookId,
                UserId = user.Id,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7), // 7 days to pick up
                Status = ReservationStatus.Pending
            };

            var result = await _reservationService.CreateReservationAsync(reservation);
            if (result != null)
            {
                return Json(new
                {
                    success = true,
                    message = "Book reserved successfully! You will be notified when it's available.",
                    expiryDate = reservation.ExpiryDate.ToString()
                });
            }

            return Json(new { success = false, message = "Failed to reserve book" });
        }

        // POST: Member/CancelReservation
        [HttpPost]
        public async Task<JsonResult> CancelReservation(int reservationId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            if (reservation == null || reservation.UserId != user.Id)
            {
                return Json(new { success = false, message = "Reservation not found" });
            }

            var result = await _reservationService.CancelReservationAsync(reservationId);
            if (result)
            {
                return Json(new { success = true, message = "Reservation cancelled successfully" });
            }

            return Json(new { success = false, message = "Failed to cancel reservation" });
        }

        // POST: Member/MarkAsReturned
        [HttpPost]
        public async Task<JsonResult> MarkAsReturned(int loanId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            var loan = await _loanService.GetLoanByIdAsync(loanId);
            if (loan == null || loan.UserId != user.Id)
            {
                return Json(new { success = false, message = "Loan not found" });
            }

            if (loan.Status != LoanStatus.Active)
            {
                return Json(new { success = false, message = "This loan is not active" });
            }

            // Return the book
            var result = await _loanService.ReturnBookAsync(loanId);
            if (result)
            {
                var updatedLoan = await _loanService.GetLoanByIdAsync(loanId);
                var lateFeeMessage = updatedLoan?.LateFee > 0 
                    ? $"Late fee: ${updatedLoan.LateFee:F2}" 
                    : "No late fee!";
                
                return Json(new
                {
                    success = true,
                    message = lateFeeMessage
                });
            }

            return Json(new { success = false, message = "Failed to mark as returned" });
        }

        // GET: Member/Profile
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        // GET: Member/EditProfile
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        // POST: Member/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ApplicationUser model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Update only allowed fields
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction(nameof(Profile));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}
