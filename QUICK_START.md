# 🚀 Quick Start Guide

## Get Running in 5 Minutes!

### 1️⃣ Open in Rider
```
Open JetBrains Rider → Open → Select LibraryManagementSystem folder
```

### 2️⃣ Create Database
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3️⃣ Run
```bash
dotnet run
```
Or press **Shift + F10** in Rider

### 4️⃣ Open Browser
```
https://localhost:5001
```

### 5️⃣ Login as Admin
```
Email: admin@library.com
Password: Admin@123
```

## 🎯 What to Test

### Test 1: Books (2 min)
1. Go to **Books**
2. Click **Add New Book**
3. Fill form and save
4. Try **Search**, **Filter**, **Sort**

### Test 2: AJAX Features (2 min)
1. Go to **Admin → Dashboard**
2. Click **Refresh Statistics** (AJAX!)
3. Click **Load Overdue Loans** (AJAX!)
4. Go to **Admin → Categories**
5. Add/Edit/Delete category (All AJAX!)

### Test 3: Authentication (1 min)
1. **Logout**
2. Click **Register**
3. Create new account
4. Try accessing Admin (denied!)

## 📋 Requirements Checklist

✅ ASP.NET Core MVC 9  
✅ 8 Tables (6+ required)  
✅ 7 Interfaces  
✅ CRUD Operations  
✅ Validators (Default + Custom)  
✅ Authentication & Authorization  
✅ jQuery AJAX  
✅ Admin Panel  
✅ Search, Filter, Sort, Pagination  

## 🔧 Common Commands

```bash
# Run project
dotnet run

# Create migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Restore packages
dotnet restore

# Build project
dotnet build
```

## 🐛 Quick Fixes

**Database Error?**
```bash
dotnet ef database drop
dotnet ef database update
```

**Port in use?**
Edit `Properties/launchSettings.json` and change port numbers.

**Packages not loading?**
```bash
dotnet restore
```

## 📁 Important Files

- `Program.cs` - App configuration
- `appsettings.json` - Connection string
- `ApplicationDbContext.cs` - Database context
- `Controllers/` - All controllers
- `Views/` - All views

## 🎨 Key URLs

- Home: `/`
- Books: `/Books`
- Login: `/Account/Login`
- Register: `/Account/Register`
- Admin Dashboard: `/Admin/Dashboard`
- Categories: `/Admin/Categories`

## 💡 Pro Tips

1. **Hot Reload** - Rider supports hot reload. Change code without restart!
2. **Database Viewer** - View → Tool Windows → Database
3. **Breakpoints** - Click left margin to add breakpoints for debugging
4. **Run Configurations** - Top-right corner to switch between Debug/Release

## 🎓 Project Highlights

- **8 Database Tables** (Books, Authors, Categories, Publishers, etc.)
- **7 Service Interfaces** (Dependency Injection)
- **3 Custom Validators** (ISBN, MinimumAge, FutureDate)
- **8 AJAX Endpoints** (Real-time updates)
- **Role-Based Access** (Admin, Librarian, Member)
- **Complete CRUD** (All entities)
- **Search & Filter** (Multiple options)
- **Pagination** (Efficient data display)

## 📚 Documentation

- **README.md** - Full documentation
- **SETUP.md** - Detailed setup guide
- **PROJECT_SUMMARY.md** - Complete project overview
- **This file** - Quick reference

## 🚀 Deploy to GitHub

```bash
git init
git add .
git commit -m "Initial commit"
git remote add origin YOUR_GITHUB_URL
git push -u origin main
```

## ✅ Before Submission

- [ ] Test all CRUD operations
- [ ] Test AJAX features
- [ ] Test authentication
- [ ] Test validation
- [ ] Check all pages load
- [ ] Verify admin access
- [ ] Test search/filter/sort
- [ ] Review code comments

## 🎯 Demo Script (5 min)

**Minute 1:** Show home page, login as admin  
**Minute 2:** Browse books, show search/filter/sort  
**Minute 3:** Add new book, show validation  
**Minute 4:** Admin dashboard, AJAX statistics  
**Minute 5:** Category management with AJAX  

## 🏆 Success!

Your project is ready! It includes:
- ✅ All required features
- ✅ Extra bonus features
- ✅ Clean, professional code
- ✅ Complete documentation
- ✅ Modern UI/UX

Good luck with your presentation! 🎉

