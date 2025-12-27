# Library Management System

A comprehensive web application built with **ASP.NET Core MVC 9** for managing library operations including books, users, loans, and reservations.

## 🚀 Features

### Core Functionality

- ✅ **Book Management** - Full CRUD operations for books with categories, publishers, and authors
- ✅ **User Management** - Registration, authentication, and role-based authorization
- ✅ **Loan System** - Track book loans with due dates and late fees
- ✅ **Reservation System** - Allow users to reserve books
- ✅ **Category Management** - Organize books by categories

### Advanced Features

- 🔍 **Search & Filter** - Search books by title, ISBN, or description
- 📊 **Sorting** - Sort by title, year, category
- 📄 **Pagination** - Efficient data display with pagination
- ⚡ **AJAX Operations** - Real-time updates without page refresh
- 🎨 **Responsive Design** - Bootstrap 5 for modern UI
- 🔐 **Role-Based Access** - Admin, Librarian, and Member roles

## 🛠️ Technologies Used

- **Framework**: ASP.NET Core MVC 9 (.NET 9)
- **Database**: SQL Server with Entity Framework Core 9
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, jQuery, Bootstrap Icons
- **Architecture**: Clean Architecture with Repository Pattern
- **Dependency Injection**: Built-in DI container

## 📊 Database Schema

The application uses **8 tables** including Identity tables:

1. **Books** - Book information
2. **Authors** - Author details
3. **Categories** - Book categories
4. **Publishers** - Publisher information
5. **BookAuthors** - Many-to-many relationship
6. **Loans** - Loan records
7. **Reservations** - Reservation records
8. **AspNetUsers** - User accounts (Identity)

Plus standard Identity tables for roles, claims, etc.

## 🔧 Setup Instructions

### Prerequisites

- .NET 9 SDK
- SQL Server (LocalDB or full version)
- Visual Studio 2022, Rider, or VS Code

### Installation Steps

1. **Clone the repository**

   ```bash
   git clone <your-repo-url>
   cd LibraryManagementSystem
   ```

2. **Update Connection String** (if needed)

   Edit `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Create Database**

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the Application**

   ```bash
   dotnet run
   ```

5. **Access the Application**

   Navigate to: `https://localhost:5001` or `http://localhost:5000`

## 👤 Default Credentials

**Admin Account:**

- Email: `admin@library.com`
- Password: `Admin@123`

## 📁 Project Structure

```
LibraryManagementSystem/
├── Controllers/          # MVC Controllers
│   ├── AccountController.cs
│   ├── BooksController.cs
│   ├── AdminController.cs
│   └── HomeController.cs
├── Models/              # Data Models
│   ├── ApplicationUser.cs
│   ├── Book.cs
│   ├── Author.cs
│   ├── Category.cs
│   ├── Publisher.cs
│   ├── Loan.cs
│   └── Reservation.cs
├── ViewModels/          # View Models
│   ├── LoginViewModel.cs
│   ├── RegisterViewModel.cs
│   └── BookViewModel.cs
├── Data/                # Database Context
│   └── ApplicationDbContext.cs
├── Interfaces/          # Service Interfaces (7+)
│   ├── IBookService.cs
│   ├── IAuthorService.cs
│   ├── ICategoryService.cs
│   ├── IPublisherService.cs
│   ├── ILoanService.cs
│   ├── IReservationService.cs
│   └── IUserService.cs
├── Services/            # Service Implementations
│   ├── BookService.cs
│   ├── AuthorService.cs
│   ├── CategoryService.cs
│   ├── PublisherService.cs
│   ├── LoanService.cs
│   ├── ReservationService.cs
│   └── UserService.cs
├── Validators/          # Custom Validators
│   ├── ISBNAttribute.cs
│   ├── MinimumAgeAttribute.cs
│   └── FutureDateAttribute.cs
└── Views/               # Razor Views
    ├── Account/
    ├── Books/
    ├── Admin/
    └── Shared/
```

## 🎯 Key Features Implementation

### 1. Authentication & Authorization

- ASP.NET Core Identity for user management
- Role-based authorization (Admin, Librarian, Member)
- Secure password hashing
- Login/Register functionality

### 2. CRUD Operations

- Full Create, Read, Update, Delete for all entities
- Form validation (client and server-side)
- Custom validators (ISBN, MinimumAge, FutureDate)

### 3. AJAX Features

- Real-time book availability check
- Dynamic category management (Create/Update/Delete)
- Overdue loans loading
- Book return processing
- Statistics refresh

### 4. Search, Filter & Sort

- Text search across book titles, ISBN, descriptions
- Filter by category
- Sort by title, year, category
- Pagination for large datasets

### 5. Admin Dashboard

- Statistics overview
- Overdue loans management
- User management
- Category management with AJAX

## 🔒 Security Features

- Password requirements (uppercase, lowercase, digit, minimum length)
- Anti-forgery tokens on forms
- Role-based access control
- SQL injection protection via EF Core
- XSS protection

## 📝 Validation

### Default Validators

- Required fields
- String length limits
- Email format
- Phone format
- URL format
- Range validation

### Custom Validators

- **ISBN Validator** - Validates ISBN-10 and ISBN-13 format
- **Minimum Age** - Ensures users meet age requirements
- **Future Date** - Prevents future dates where not applicable

## 🌐 API Endpoints (AJAX)

- `GET /Books/CheckAvailability/{id}` - Check book availability
- `GET /Books/SearchBooks?term={term}` - Search books
- `GET /Admin/GetStatistics` - Get dashboard statistics
- `GET /Admin/GetOverdueLoans` - Get overdue loans
- `POST /Admin/ReturnBook` - Return a book
- `POST /Admin/CreateCategory` - Create category
- `POST /Admin/UpdateCategory` - Update category
- `POST /Admin/DeleteCategory` - Delete category

## 🎨 UI Components

- Responsive navigation bar
- Card-based book display
- Modal dialogs for forms
- Alert notifications
- Loading spinners
- Bootstrap icons
- Data tables

## 📦 NuGet Packages

- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.EntityFrameworkCore.Tools (9.0.0)
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)

## 🤝 Contributing

This is a student project for learning purposes. Feel free to fork and modify for your own educational needs.

## 📄 License

This project is for educational purposes.

## 👨‍💻 Author

Created as part of .NET Technology course requirements.

## 📧 Support

For questions or issues, please open an issue on GitHub.

---

**Note**: This project fulfills all requirements including:

- ✅ ASP.NET Core MVC 9
- ✅ 6+ tables (8 total including Identity)
- ✅ 7+ interfaces
- ✅ Full CRUD operations
- ✅ Default and custom validators
- ✅ Authentication, authorization, registration
- ✅ jQuery AJAX functionality
- ✅ Admin panel
- ✅ Filtering, sorting, pagination
