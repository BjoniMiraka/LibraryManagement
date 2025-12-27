# Setup Guide for Library Management System

## Quick Start Guide

Follow these steps to get the project running on your machine.

### Step 1: Open in JetBrains Rider (or your preferred IDE)

1. Open **JetBrains Rider**
2. Click **Open** and navigate to the `LibraryManagementSystem` folder
3. Select the `LibraryManagementSystem.csproj` file or the folder
4. Wait for Rider to restore NuGet packages

### Step 2: Update Connection String (Optional)

If you're not using SQL Server LocalDB, update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=LibraryManagementDB;Trusted_Connection=True;"
}
```

For **Mac users**, you might want to use SQLite instead:

1. Install SQLite package:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 9.0.0
   ```

2. Update `Program.cs` line 14:
   ```csharp
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
   ```

3. Update `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=library.db"
   }
   ```

### Step 3: Create the Database

Open the terminal in Rider (or your IDE) and run:

```bash
# Create migration
dotnet ef migrations add InitialCreate

# Apply migration to create database
dotnet ef database update
```

**Note**: If you don't have `dotnet ef` tools installed:
```bash
dotnet tool install --global dotnet-ef
```

### Step 4: Run the Application

In Rider:
- Press **Shift + F10** (Windows/Linux) or **Control + R** (Mac)
- Or click the green **Run** button

In Terminal:
```bash
dotnet run
```

### Step 5: Access the Application

Open your browser and navigate to:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000

### Step 6: Login with Admin Account

Use these credentials to access the admin panel:
- **Email**: admin@library.com
- **Password**: Admin@123

## Troubleshooting

### Issue: Database connection error

**Solution**: Make sure SQL Server is running, or switch to SQLite (see Step 2).

### Issue: Migration error

**Solution**: Delete the `Migrations` folder and `library.db` file (if using SQLite), then run migrations again.

### Issue: Port already in use

**Solution**: Change the port in `Properties/launchSettings.json`.

### Issue: NuGet packages not restoring

**Solution**: Run `dotnet restore` in the terminal.

## Git Setup

### Initialize Git Repository

```bash
cd LibraryManagementSystem
git init
git add .
git commit -m "Initial commit: Library Management System"
```

### Push to GitHub

1. Create a new repository on GitHub
2. Run these commands:

```bash
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git
git branch -M main
git push -u origin main
```

## Project Features Checklist

✅ ASP.NET Core MVC 9  
✅ 8 Database Tables (including Identity)  
✅ 7+ Interfaces  
✅ Full CRUD Operations  
✅ Authentication & Authorization  
✅ Registration System  
✅ Default Validators (Required, Email, Phone, etc.)  
✅ Custom Validators (ISBN, MinimumAge, FutureDate)  
✅ jQuery AJAX Features  
✅ Admin Panel  
✅ Search & Filter  
✅ Sorting  
✅ Pagination  

## Testing the Application

### Test Authentication
1. Register a new user
2. Login with the new user
3. Try accessing admin features (should be denied)
4. Logout and login as admin

### Test Book Management
1. Login as admin
2. Go to Books → Add New Book
3. Fill in the form and create a book
4. Edit the book
5. Search for the book
6. Filter by category
7. Sort by different fields

### Test AJAX Features
1. Go to Admin → Dashboard
2. Click "Refresh Statistics" (AJAX)
3. Click "Load Overdue Loans" (AJAX)
4. Go to Admin → Categories
5. Add a new category (AJAX)
6. Edit a category (AJAX)
7. Delete a category (AJAX)

### Test Validation
1. Try to create a book with invalid ISBN
2. Try to register with weak password
3. Try to create a book without required fields

## Development Tips

### Using Rider
- **Run**: Shift + F10 (Windows/Linux) or Control + R (Mac)
- **Debug**: Shift + F9 (Windows/Linux) or Control + D (Mac)
- **Stop**: Ctrl + F2 (Windows/Linux) or Command + F2 (Mac)

### Hot Reload
Rider supports hot reload. Make changes to your code and they'll be reflected without restarting.

### Database Viewer
Rider has a built-in database viewer. Go to **View → Tool Windows → Database** to explore your database.

## Need Help?

- Check the README.md for detailed documentation
- Review the code comments
- Check the project structure in SETUP.md

## Next Steps

After getting the project running:
1. Explore the codebase
2. Add more features (e.g., book reviews, ratings)
3. Customize the UI
4. Add more AJAX functionality
5. Implement additional reports

Good luck with your project! 🚀

