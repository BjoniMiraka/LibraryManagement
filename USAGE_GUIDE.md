# 📖 Library Management System - Usage Guide

## 🚀 Getting Started

### Start the Application
```bash
cd LibraryManagementSystem
dotnet run
```

The application will start on: **http://localhost:5283**

---

## 👤 User Accounts

### Default Admin Account
- **Email**: `admin@library.com`
- **Password**: `Admin@123`
- **Role**: Admin (full access)

### Create Member Account
1. Go to http://localhost:5283
2. Click "Get Started" or "Register"
3. Fill in the registration form:
   - First Name
   - Last Name
   - Email
   - Password (min 6 chars, uppercase, lowercase, digit)
   - Confirm Password
   - Address
   - Date of Birth (must be 13+ years old)
   - Phone Number
4. Click "Register"
5. You'll be automatically logged in as a Member

---

## 🔐 Login

1. Go to http://localhost:5283/Account/Login
2. Enter email and password
3. Click "Login"
4. You'll be redirected based on your role:
   - **Admin**: Can access everything
   - **Member**: Can browse, borrow, and reserve books

---

## 📚 For Members

### Browse Books
1. Click "Books" in navigation
2. Use search box to find books by title, ISBN, or description
3. Filter by category using dropdown
4. Sort by title, year, or category
5. Click on a book to view details

### Borrow a Book
1. Go to book details page
2. If book is available, click "Borrow This Book"
3. Confirm the action
4. Book will be loaned for 14 days
5. View your loans in "My Account" → "My Loans"

### Reserve a Book
1. Go to book details page
2. If book is NOT available, click "Reserve This Book"
3. Confirm the action
4. You'll be notified when it becomes available
5. You have 7 days to pick it up
6. View your reservations in "My Account" → "My Reservations"

### View Your Loans
1. Click "My Account" → "My Loans"
2. See all your current and past loans
3. Check due dates
4. See if any books are overdue (red badge)
5. View late fees if applicable

### Manage Reservations
1. Click "My Account" → "My Reservations"
2. See all your reservations
3. Cancel a reservation if needed

### View Your Profile
1. Click "My Account" → "My Profile"
2. See your personal information
3. Check your member since date

---

## 👨‍💼 For Admins

### Access Admin Dashboard
1. Login as admin
2. Click "Admin" → "Dashboard"
3. View statistics:
   - Total Books
   - Total Users
   - Active Loans
   - Overdue Loans
4. Click "Refresh Statistics" to update numbers
5. Click "Load Overdue Loans" to see overdue items
6. Process returns directly from dashboard

### Manage Users
1. Click "Admin" → "Users"
2. View all registered users
3. Click "View Details" to see:
   - User information
   - Active loans
   - Reservations

### Manage Loans
1. Click "Admin" → "Loans"
2. View all loans in the system
3. Filter by status (Active, Returned, Overdue)
4. Click "Return" to process a book return
5. System automatically calculates late fees

### Manage Categories
1. Click "Admin" → "Categories"
2. View all categories
3. Click "Add Category" to create new one
4. Click "Edit" to modify a category
5. Click "Delete" to remove a category
6. All actions use AJAX (no page reload)

### Manage Books

#### Add a New Book
1. Click "Books" → "Create" (or from home page)
2. Fill in book details:
   - Title (required)
   - ISBN (required, 13 digits)
   - Category (select from dropdown)
   - Publisher (select from dropdown)
   - Published Year
   - Total Copies
   - Description
   - Cover Image URL
3. Click "Create"

#### Edit a Book
1. Go to book details page
2. Click "Edit" button
3. Modify the information
4. Click "Save Changes"

#### Delete a Book
1. Go to book details page
2. Click "Delete" button (Admin only)
3. Confirm deletion
4. Book will be removed from system

---

## 🎯 Common Workflows

### Scenario 1: Member Borrows a Book
1. Member logs in
2. Browses books
3. Finds an available book
4. Clicks "Borrow This Book"
5. Book is loaned for 14 days
6. Available copies decrease by 1
7. Member can track loan in "My Loans"

### Scenario 2: Member Reserves an Unavailable Book
1. Member finds a book with 0 available copies
2. Clicks "Reserve This Book"
3. Reservation is created
4. When someone returns the book:
   - Member is notified
   - Member has 7 days to pick it up

### Scenario 3: Admin Processes a Return
1. Admin goes to Dashboard
2. Clicks "Load Overdue Loans" (or goes to Loans page)
3. Finds the loan to return
4. Clicks "Return" button
5. System calculates late fee if overdue
6. Book's available copies increase by 1
7. Loan status changes to "Returned"

### Scenario 4: Admin Adds New Books
1. Admin logs in
2. Clicks "Books" → "Create"
3. Enters book information
4. Submits form
5. Book appears in catalog
6. Members can now borrow it

---

## ⚠️ Important Notes

### Late Fees
- **Rate**: $1 per day after due date
- **Calculation**: Automatic when book is returned
- **Display**: Shows in loan history

### Loan Period
- **Duration**: 14 days from borrow date
- **Overdue**: Automatically marked after due date
- **Limits**: One active loan per book per user

### Reservation Rules
- **Only for unavailable books**: Can't reserve if copies are available
- **Pickup window**: 7 days from reservation
- **Expiry**: Automatically expires after 7 days
- **Limits**: One reservation per book per user

### Book Availability
- **Total Copies**: How many the library owns
- **Available Copies**: How many can be borrowed now
- **Updates**: Automatically when borrowed/returned

---

## 🔍 Search & Filter Tips

### Search Books
- Search by **title**: "1984"
- Search by **ISBN**: "978-0451524935"
- Search by **description**: "dystopian"
- Partial matches work!

### Filter by Category
- Use dropdown to select category
- Shows only books in that category
- Combine with search for better results

### Sort Options
- **Title** (A-Z)
- **Title Descending** (Z-A)
- **Year** (oldest first)
- **Year Descending** (newest first)
- **Category** (alphabetical)

---

## 🎨 UI Elements

### Color Codes
- **Blue**: Primary actions, active items
- **Green**: Success, available, active loans
- **Yellow/Orange**: Warnings, edit actions
- **Red**: Danger, overdue, delete actions
- **Gray**: Secondary actions, returned items
- **Info**: Reservations, information

### Icons
- 📚 **Book**: Books and catalog
- 👤 **Person**: User profile and account
- ⚙️ **Gear**: Settings and admin
- ✓ **Check**: Confirm and success
- ✗ **X**: Cancel and close
- 🔍 **Search**: Search functionality
- 📊 **Chart**: Statistics and dashboard

---

## 🆘 Troubleshooting

### Can't Login?
- Check email and password
- Password is case-sensitive
- Use admin@library.com / Admin@123 for admin access

### Can't Borrow a Book?
- Check if book is available (Available Copies > 0)
- Check if you already have this book on loan
- Make sure you're logged in

### Can't Reserve a Book?
- Reservations only work for unavailable books
- Check if you already have a reservation for this book
- Make sure you're logged in

### Page Not Loading?
- Make sure the application is running (dotnet run)
- Check the URL: http://localhost:5283
- Try refreshing the page

---

## 📞 Features Summary

✅ User registration and login
✅ Browse and search books
✅ Borrow books (14-day period)
✅ Reserve unavailable books
✅ Track loans and due dates
✅ Manage reservations
✅ Admin dashboard with statistics
✅ User management
✅ Loan management with late fees
✅ Category management (AJAX)
✅ Book CRUD operations
✅ Role-based access control
✅ Responsive design
✅ Real-time updates

---

## 🎉 Enjoy Your Library Management System!

The system is fully functional and ready to use. Explore all features and manage your library efficiently!
