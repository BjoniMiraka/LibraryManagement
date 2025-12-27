using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;

        public BooksController(
            IBookService bookService,
            ICategoryService categoryService,
            IPublisherService publisherService,
            IAuthorService authorService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _publisherService = publisherService;
            _authorService = authorService;
        }

        // GET: Books
        public async Task<IActionResult> Index(string? searchTerm, int? categoryId, int page = 1, int pageSize = 10, string sortBy = "Title")
        {
            IEnumerable<Book> books;

            // Search functionality
            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = await _bookService.SearchBooksAsync(searchTerm);
            }
            // Filter by category
            else if (categoryId.HasValue)
            {
                books = await _bookService.GetBooksByCategoryAsync(categoryId.Value);
            }
            else
            {
                books = await _bookService.GetAllBooksAsync();
            }

            // Sorting
            books = sortBy switch
            {
                "Title" => books.OrderBy(b => b.Title),
                "TitleDesc" => books.OrderByDescending(b => b.Title),
                "Year" => books.OrderBy(b => b.PublishedYear),
                "YearDesc" => books.OrderByDescending(b => b.PublishedYear),
                "Category" => books.OrderBy(b => b.Category.Name),
                _ => books.OrderBy(b => b.Title)
            };

            // Pagination
            var totalItems = books.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            books = books.Skip((page - 1) * pageSize).Take(pageSize);

            // Pass data to view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            ViewBag.SortBy = sortBy;
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Title,
                    ISBN = model.ISBN,
                    PublishedYear = model.PublishedYear,
                    Description = model.Description,
                    TotalCopies = model.TotalCopies,
                    CoverImageUrl = model.CoverImageUrl,
                    CategoryId = model.CategoryId,
                    PublisherId = model.PublisherId
                };

                await _bookService.CreateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdownsAsync();
            return View(model);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var model = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedYear = book.PublishedYear,
                Description = book.Description,
                TotalCopies = book.TotalCopies,
                CoverImageUrl = book.CoverImageUrl,
                CategoryId = book.CategoryId,
                PublisherId = book.PublisherId
            };

            await PopulateDropdownsAsync();
            return View(model);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Edit(int id, BookViewModel model)
        {
            if (id != model.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                book.Title = model.Title;
                book.ISBN = model.ISBN;
                book.PublishedYear = model.PublishedYear;
                book.Description = model.Description;
                book.TotalCopies = model.TotalCopies;
                book.CoverImageUrl = model.CoverImageUrl;
                book.CategoryId = model.CategoryId;
                book.PublisherId = model.PublisherId;

                await _bookService.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdownsAsync();
            return View(model);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // AJAX: Check book availability
        [HttpGet]
        public async Task<JsonResult> CheckAvailability(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }

            return Json(new
            {
                success = true,
                available = book.AvailableCopies > 0,
                availableCopies = book.AvailableCopies,
                totalCopies = book.TotalCopies
            });
        }

        // AJAX: Search books
        [HttpGet]
        public async Task<JsonResult> SearchBooks(string term)
        {
            var books = await _bookService.SearchBooksAsync(term);
            var result = books.Select(b => new
            {
                id = b.BookId,
                title = b.Title,
                isbn = b.ISBN,
                author = string.Join(", ", b.BookAuthors.Select(ba => ba.Author.FullName)),
                category = b.Category.Name,
                available = b.AvailableCopies > 0
            });

            return Json(result);
        }

        private async Task PopulateDropdownsAsync()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
            ViewBag.Publishers = new SelectList(await _publisherService.GetAllPublishersAsync(), "PublisherId", "Name");
            ViewBag.Authors = new SelectList(await _authorService.GetAllAuthorsAsync(), "AuthorId", "FullName");
        }
    }
}

