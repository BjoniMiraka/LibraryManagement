using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Database (using SQLite for Mac compatibility)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure Application Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.SlidingExpiration = true;
});

// Register Services with Dependency Injection
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add Session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Apply migrations and seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Create database if it doesn't exist
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
        
        // Seed roles and admin user
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        await SeedRolesAndAdminAsync(roleManager, userManager);
        
        // Seed dummy data for testing
        await SeedDummyDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while setting up the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Method to seed roles and admin user
async Task SeedRolesAndAdminAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    // Create roles if they don't exist
    string[] roleNames = { "Admin", "Librarian", "Member" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Create default admin user
    var adminEmail = "admin@library.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
            LastName = "User",
            EmailConfirmed = true,
            DateOfBirth = new DateTime(1990, 1, 1),
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(admin, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}

// Method to seed dummy data for testing
async Task SeedDummyDataAsync(ApplicationDbContext context)
{
    // Check if we already have data
    if (context.Books.Any())
    {
        return; // Database already seeded
    }

    // Seed Categories
    var categories = new[]
    {
        new Category { Name = "Fiction", Description = "Fictional stories and novels", CreatedAt = DateTime.UtcNow },
        new Category { Name = "Non-Fiction", Description = "Real-world topics and facts", CreatedAt = DateTime.UtcNow },
        new Category { Name = "Science", Description = "Scientific books and research", CreatedAt = DateTime.UtcNow },
        new Category { Name = "History", Description = "Historical events and figures", CreatedAt = DateTime.UtcNow },
        new Category { Name = "Technology", Description = "Technology and programming", CreatedAt = DateTime.UtcNow }
    };
    context.Categories.AddRange(categories);
    await context.SaveChangesAsync();

    // Seed Publishers
    var publishers = new[]
    {
        new Publisher { Name = "Penguin Books", Address = "New York, USA", Email = "info@penguin.com", Phone = "+1-555-0100", CreatedAt = DateTime.UtcNow },
        new Publisher { Name = "HarperCollins", Address = "London, UK", Email = "info@harpercollins.com", Phone = "+44-20-8741-7070", CreatedAt = DateTime.UtcNow },
        new Publisher { Name = "Random House", Address = "New York, USA", Email = "info@randomhouse.com", Phone = "+1-555-0200", CreatedAt = DateTime.UtcNow },
        new Publisher { Name = "O'Reilly Media", Address = "California, USA", Email = "info@oreilly.com", Phone = "+1-555-0300", CreatedAt = DateTime.UtcNow }
    };
    context.Publishers.AddRange(publishers);
    await context.SaveChangesAsync();

    // Seed Authors
    var authors = new[]
    {
        new Author { FirstName = "George", LastName = "Orwell", Biography = "English novelist and essayist", Country = "United Kingdom", DateOfBirth = new DateTime(1903, 6, 25), CreatedAt = DateTime.UtcNow },
        new Author { FirstName = "Jane", LastName = "Austen", Biography = "English novelist", Country = "United Kingdom", DateOfBirth = new DateTime(1775, 12, 16), CreatedAt = DateTime.UtcNow },
        new Author { FirstName = "Stephen", LastName = "Hawking", Biography = "Theoretical physicist and cosmologist", Country = "United Kingdom", DateOfBirth = new DateTime(1942, 1, 8), CreatedAt = DateTime.UtcNow },
        new Author { FirstName = "Yuval Noah", LastName = "Harari", Biography = "Israeli historian and philosopher", Country = "Israel", DateOfBirth = new DateTime(1976, 2, 24), CreatedAt = DateTime.UtcNow },
        new Author { FirstName = "Robert C.", LastName = "Martin", Biography = "Software engineer and author", Country = "United States", DateOfBirth = new DateTime(1952, 12, 5), CreatedAt = DateTime.UtcNow }
    };
    context.Authors.AddRange(authors);
    await context.SaveChangesAsync();

    // Seed Books
    var books = new[]
    {
        new Book
        {
            Title = "1984",
            ISBN = "978-0451524935",
            PublishedYear = 1949,
            Description = "A dystopian social science fiction novel and cautionary tale about the dangers of totalitarianism.",
            TotalCopies = 5,
            AvailableCopies = 5,
            CategoryId = categories[0].CategoryId,
            PublisherId = publishers[0].PublisherId,
            CoverImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71kxa1-0mfL.jpg",
            CreatedAt = DateTime.UtcNow
        },
        new Book
        {
            Title = "Pride and Prejudice",
            ISBN = "978-0141439518",
            PublishedYear = 1813,
            Description = "A romantic novel of manners that follows the character development of Elizabeth Bennet.",
            TotalCopies = 4,
            AvailableCopies = 4,
            CategoryId = categories[0].CategoryId,
            PublisherId = publishers[1].PublisherId,
            CoverImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71Q1tPupKjL.jpg",
            CreatedAt = DateTime.UtcNow
        },
        new Book
        {
            Title = "A Brief History of Time",
            ISBN = "978-0553380163",
            PublishedYear = 1988,
            Description = "A landmark volume in science writing by one of the great minds of our time, Stephen Hawking.",
            TotalCopies = 3,
            AvailableCopies = 3,
            CategoryId = categories[2].CategoryId,
            PublisherId = publishers[2].PublisherId,
            CoverImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81SbcDjN5LL.jpg",
            CreatedAt = DateTime.UtcNow
        },
        new Book
        {
            Title = "Sapiens: A Brief History of Humankind",
            ISBN = "978-0062316097",
            PublishedYear = 2011,
            Description = "Explores the history of humankind from the Stone Age to the modern age.",
            TotalCopies = 6,
            AvailableCopies = 6,
            CategoryId = categories[3].CategoryId,
            PublisherId = publishers[1].PublisherId,
            CoverImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71f5Yqd9fdL.jpg",
            CreatedAt = DateTime.UtcNow
        },
        new Book
        {
            Title = "Clean Code",
            ISBN = "978-0132350884",
            PublishedYear = 2008,
            Description = "A Handbook of Agile Software Craftsmanship with practical advice on writing clean, maintainable code.",
            TotalCopies = 4,
            AvailableCopies = 4,
            CategoryId = categories[4].CategoryId,
            PublisherId = publishers[3].PublisherId,
            CoverImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41xShlnTZTL.jpg",
            CreatedAt = DateTime.UtcNow
        },
        new Book
        {
            Title = "Animal Farm",
            ISBN = "978-0451526342",
            PublishedYear = 1945,
            Description = "A satirical allegorical novella reflecting events leading up to the Russian Revolution.",
            TotalCopies = 5,
            AvailableCopies = 5,
            CategoryId = categories[0].CategoryId,
            PublisherId = publishers[0].PublisherId,
            CoverImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71JNXa7TPUL.jpg",
            CreatedAt = DateTime.UtcNow
        }
    };
    context.Books.AddRange(books);
    await context.SaveChangesAsync();

    // Seed BookAuthors (relationships)
    var bookAuthors = new[]
    {
        new BookAuthor { BookId = books[0].BookId, AuthorId = authors[0].AuthorId, CreatedAt = DateTime.UtcNow }, // 1984 - George Orwell
        new BookAuthor { BookId = books[1].BookId, AuthorId = authors[1].AuthorId, CreatedAt = DateTime.UtcNow }, // Pride and Prejudice - Jane Austen
        new BookAuthor { BookId = books[2].BookId, AuthorId = authors[2].AuthorId, CreatedAt = DateTime.UtcNow }, // A Brief History of Time - Stephen Hawking
        new BookAuthor { BookId = books[3].BookId, AuthorId = authors[3].AuthorId, CreatedAt = DateTime.UtcNow }, // Sapiens - Yuval Noah Harari
        new BookAuthor { BookId = books[4].BookId, AuthorId = authors[4].AuthorId, CreatedAt = DateTime.UtcNow }, // Clean Code - Robert C. Martin
        new BookAuthor { BookId = books[5].BookId, AuthorId = authors[0].AuthorId, CreatedAt = DateTime.UtcNow }  // Animal Farm - George Orwell
    };
    context.BookAuthors.AddRange(bookAuthors);
    await context.SaveChangesAsync();
}
