using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly ILoanService _loanService;
        private readonly IReservationService _reservationService;
        private readonly ICategoryService _categoryService;

        public AdminController(
            IBookService bookService,
            IUserService userService,
            ILoanService loanService,
            IReservationService reservationService,
            ICategoryService categoryService)
        {
            _bookService = bookService;
            _userService = userService;
            _loanService = loanService;
            _reservationService = reservationService;
            _categoryService = categoryService;
        }

        // GET: Admin/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var books = await _bookService.GetAllBooksAsync();
            var users = await _userService.GetAllUsersAsync();
            var loans = await _loanService.GetAllLoansAsync();
            var overdueLoans = await _loanService.GetOverdueLoansAsync();

            ViewBag.TotalBooks = books.Count();
            ViewBag.TotalUsers = users.Count();
            ViewBag.ActiveLoans = loans.Count(l => l.Status == LoanStatus.Active);
            ViewBag.OverdueLoans = overdueLoans.Count();

            return View();
        }

        // GET: Admin/Users
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // AJAX: Get user details
        [HttpGet]
        public async Task<JsonResult> GetUserDetails(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            var loans = await _loanService.GetLoansByUserAsync(id);
            var reservations = await _reservationService.GetReservationsByUserAsync(id);

            return Json(new
            {
                success = true,
                user = new
                {
                    id = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    address = user.Address,
                    dateOfBirth = user.DateOfBirth.ToString("yyyy-MM-dd"),
                    createdAt = user.CreatedAt.ToString("yyyy-MM-dd")
                },
                loans = loans.Select(l => new
                {
                    loanId = l.LoanId,
                    bookTitle = l.Book.Title,
                    loanDate = l.LoanDate.ToString("yyyy-MM-dd"),
                    dueDate = l.DueDate.ToString("yyyy-MM-dd"),
                    status = l.Status.ToString(),
                    isOverdue = l.IsOverdue
                }),
                reservations = reservations.Select(r => new
                {
                    reservationId = r.ReservationId,
                    bookTitle = r.Book.Title,
                    reservationDate = r.ReservationDate.ToString("yyyy-MM-dd"),
                    status = r.Status.ToString()
                })
            });
        }

        // GET: Admin/Loans
        public async Task<IActionResult> Loans()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return View(loans);
        }

        // AJAX: Get overdue loans
        [HttpGet]
        public async Task<JsonResult> GetOverdueLoans()
        {
            var overdueLoans = await _loanService.GetOverdueLoansAsync();
            var result = overdueLoans.Select(l => new
            {
                loanId = l.LoanId,
                bookTitle = l.Book.Title,
                userName = $"{l.User.FirstName} {l.User.LastName}",
                userEmail = l.User.Email,
                loanDate = l.LoanDate.ToString("yyyy-MM-dd"),
                dueDate = l.DueDate.ToString("yyyy-MM-dd"),
                daysOverdue = (DateTime.UtcNow - l.DueDate).Days
            });

            return Json(new { success = true, loans = result });
        }

        // AJAX: Return book
        [HttpPost]
        public async Task<JsonResult> ReturnBook(int loanId)
        {
            var result = await _loanService.ReturnBookAsync(loanId);
            if (result)
            {
                var loan = await _loanService.GetLoanByIdAsync(loanId);
                return Json(new
                {
                    success = true,
                    message = "Book returned successfully",
                    lateFee = loan?.LateFee ?? 0
                });
            }

            return Json(new { success = false, message = "Failed to return book" });
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Categories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        // AJAX: Create category
        [HttpPost]
        public async Task<JsonResult> CreateCategory([FromBody] Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                return Json(new { success = false, message = "Category name is required" });
            }

            try
            {
                var newCategory = await _categoryService.CreateCategoryAsync(category);
                return Json(new
                {
                    success = true,
                    message = "Category created successfully",
                    category = new
                    {
                        categoryId = newCategory.CategoryId,
                        name = newCategory.Name,
                        description = newCategory.Description
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // AJAX: Update category
        [HttpPost]
        public async Task<JsonResult> UpdateCategory([FromBody] Category category)
        {
            if (category.CategoryId <= 0 || string.IsNullOrWhiteSpace(category.Name))
            {
                return Json(new { success = false, message = "Invalid category data" });
            }

            try
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Json(new { success = true, message = "Category updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // AJAX: Delete category
        [HttpPost]
        public async Task<JsonResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Category deleted successfully" });
                }
                return Json(new { success = false, message = "Category not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // AJAX: Get statistics
        [HttpGet]
        public async Task<JsonResult> GetStatistics()
        {
            var books = await _bookService.GetAllBooksAsync();
            var users = await _userService.GetAllUsersAsync();
            var loans = await _loanService.GetAllLoansAsync();
            var overdueLoans = await _loanService.GetOverdueLoansAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();

            return Json(new
            {
                success = true,
                statistics = new
                {
                    totalBooks = books.Count(),
                    totalUsers = users.Count(),
                    activeLoans = loans.Count(l => l.Status == LoanStatus.Active),
                    overdueLoans = overdueLoans.Count(),
                    totalCategories = categories.Count(),
                    availableBooks = books.Sum(b => b.AvailableCopies),
                    booksOnLoan = books.Sum(b => b.TotalCopies - b.AvailableCopies)
                }
            });
        }
    }
}

