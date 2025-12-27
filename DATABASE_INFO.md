# 📊 Database Information

## 📍 Database Location

Your database is a **SQLite file** located at:

```
/Users/bjoni/Desktop/dotNetProject/LibraryManagementSystem/library.db
```

**Full Path:** `/Users/bjoni/Desktop/dotNetProject/LibraryManagementSystem/library.db`

## ✅ Yes, Your Data is PERMANENT!

### **What Happens When You Turn Off and Restart?**

✅ **All your data is SAVED!** 

- The `library.db` file is a **real database file** stored on your hard drive
- When you stop the application, the database file stays on disk
- When you restart the application, it connects to the same database file
- **All books, users, categories, loans, etc. are preserved**

### **How It Works:**

```
Start App → Connects to library.db → Read/Write Data → Stop App
                     ↓
            library.db stays on disk
                     ↓
Start App Again → Connects to same library.db → All data is there!
```

## 🔍 What's in the Database Right Now?

Your database currently contains:

### **Seed Data (Pre-loaded):**
- ✅ **3 Authors** (George Orwell, Jane Austen, Stephen Hawking)
- ✅ **5 Categories** (Fiction, Non-Fiction, Science, History, Technology)
- ✅ **3 Publishers** (Penguin Books, HarperCollins, O'Reilly Media)
- ✅ **1 Admin User** (admin@library.com)
- ✅ **3 Roles** (Admin, Librarian, Member)

### **Empty Tables (Ready for Your Data):**
- 📚 **Books** - Add your books here
- 👥 **Users** - New users who register
- 📖 **Loans** - Book loans you create
- 🔖 **Reservations** - Book reservations

## 💾 Database File Details

```bash
File: library.db
Size: 4.0 KB (will grow as you add data)
Type: SQLite database
Location: Same folder as your project
```

## 🧪 Testing Data Persistence

### **Try This:**

1. **Add a Book:**
   - Login as admin
   - Go to Books → Add New Book
   - Create a book (e.g., "Test Book")

2. **Stop the Application:**
   ```bash
   # Press Ctrl+C in terminal or kill the process
   ```

3. **Restart the Application:**
   ```bash
   cd /Users/bjoni/Desktop/dotNetProject/LibraryManagementSystem
   dotnet run
   ```

4. **Check Your Data:**
   - Login again
   - Go to Books
   - **Your "Test Book" will still be there!** ✅

## 📂 Viewing the Database

You can view the database contents using:

### **Option 1: DB Browser for SQLite (Recommended)**
- Download: https://sqlitebrowser.org/
- Open `library.db` file
- View all tables and data

### **Option 2: JetBrains Rider (Built-in)**
- In Rider: View → Tool Windows → Database
- Add Data Source → SQLite
- Select `library.db` file
- Browse all tables

### **Option 3: Command Line**
```bash
# Install sqlite3 (if not already installed)
brew install sqlite3

# Open database
sqlite3 library.db

# View tables
.tables

# View data
SELECT * FROM Books;
SELECT * FROM Categories;
SELECT * FROM AspNetUsers;

# Exit
.quit
```

## 🔄 Database Lifecycle

### **When Database is Created:**
- First time you run `dotnet run`
- The `library.db` file is created
- All tables are created
- Seed data is inserted

### **When Database is Used:**
- Every time the app runs, it connects to `library.db`
- All CRUD operations write to this file
- Data is saved immediately (SQLite auto-commits)

### **When You Want to Reset:**
If you want to start fresh:

```bash
# Stop the application first!
# Then delete the database file
rm library.db

# Run the app again - it will create a new database
dotnet run
```

⚠️ **Warning:** Deleting `library.db` will **permanently delete all your data**!

## 📊 Database Schema

Your database has **8 main tables:**

1. **Books** - Book information
2. **Authors** - Author details
3. **Categories** - Book categories
4. **Publishers** - Publisher information
5. **BookAuthors** - Links books to authors
6. **Loans** - Tracks book loans
7. **Reservations** - Book reservations
8. **AspNetUsers** - User accounts

Plus 5 Identity tables for authentication.

## 🔐 Database Security

- ✅ Database is stored locally on your machine
- ✅ Passwords are hashed (not stored in plain text)
- ✅ No remote access (only your app can access it)
- ✅ File permissions protect the database

## 💡 Important Notes

### **Backup Your Database:**
```bash
# Create a backup
cp library.db library.db.backup

# Restore from backup
cp library.db.backup library.db
```

### **Database File is Portable:**
- You can copy `library.db` to another computer
- The app will work with the copied database
- Great for sharing data or moving projects

### **Git and Database:**
- The `.gitignore` file excludes `*.db` files
- Your database won't be committed to Git
- This is intentional (databases shouldn't be in version control)

## 🎯 Summary

**Q: Where is the database?**  
**A:** `/Users/bjoni/Desktop/dotNetProject/LibraryManagementSystem/library.db`

**Q: Will books be saved when I restart?**  
**A:** **YES!** All data is permanently saved in the `library.db` file.

**Q: How do I reset the database?**  
**A:** Stop the app, delete `library.db`, restart the app.

**Q: Can I view the database?**  
**A:** Yes! Use DB Browser for SQLite, Rider's database tools, or sqlite3 command.

---

**Your data is safe and persistent!** 🎉

