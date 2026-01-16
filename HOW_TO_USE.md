# 📖 How to Use the Library Management System

## 🎯 Simple Explanation

### What is this system?
This is a **digital library** where you can borrow books online, just like a real library!

---

## 📚 Understanding Key Concepts

### 1. **What are LOANS?** (Borrowing Books)

**A loan = You borrowed a book**

When you see a book you like and it's **available**, you can borrow it:

1. Click on the book
2. Click "**Borrow This Book**" button
3. The book is yours for **14 days**!

**Where to see your loans:**
- Go to "**My Account**" → "**My Loans**"
- You'll see:
  - Which books you borrowed
  - When you borrowed them (Loan Date)
  - When to return them (Due Date)
  - If you returned them (Return Date)

**Important:**
- ⏰ You have **14 days** to read the book
- 💰 If you return late: **$1 per day** late fee
- ✅ Return on time = No fees!

**Example:**
```
You borrow "Harry Potter" on January 1st
→ Due date is January 15th (14 days later)
→ Return it by January 15th = FREE
→ Return it on January 20th = $5 late fee (5 days late)
```

---

### 2. **What are RESERVATIONS?** (Waiting List)

**A reservation = You're waiting for a book**

Sometimes all copies of a book are borrowed by other people. You can join the waiting list:

1. Find a book that says "**Not Available**"
2. Click "**Reserve This Book**" button
3. Wait for someone to return it
4. You'll be notified when it's ready!
5. You have **7 days** to pick it up

**Where to see your reservations:**
- Go to "**My Account**" → "**My Reservations**"
- You'll see:
  - Which books you're waiting for
  - When you reserved them
  - When the reservation expires
  - Status (Pending, Fulfilled, Cancelled, Expired)

**Reservation Status Explained:**
- 🟡 **Pending**: Still waiting, book not available yet
- 🟢 **Fulfilled**: Book is ready! Go borrow it now!
- ⚪ **Cancelled**: You cancelled it
- 🔴 **Expired**: You didn't pick it up in time (7 days passed)

**Example:**
```
"1984" has 0 available copies (all borrowed)
→ You click "Reserve This Book"
→ Status: Pending (you're in the waiting list)
→ Someone returns "1984"
→ Status: Fulfilled (it's ready for you!)
→ You have 7 days to borrow it
→ If you don't borrow it in 7 days: Status becomes Expired
```

---

## 🚀 Step-by-Step Guide

### **Getting Started**

#### 1. Register (First Time)
1. Go to http://localhost:5283
2. Click "**Get Started**" or "**Register**"
3. Fill in your information:
   - First Name & Last Name
   - Email (this is your username)
   - Password (at least 6 characters)
   - Address
   - Date of Birth (must be 13+ years old)
   - Phone Number
4. Click "**Register**"
5. You're automatically logged in!

#### 2. Login (Returning Users)
1. Go to http://localhost:5283
2. Click "**Login**"
3. Enter your email and password
4. Click "**Login**"

---

### **Borrowing Books**

#### Step 1: Browse Books
1. Click "**Books**" in the top menu
2. You'll see all available books

#### Step 2: Search for Books
- Use the **search box** to find books by:
  - Title (e.g., "Harry Potter")
  - ISBN (e.g., "978-0451524935")
  - Description (e.g., "magic")

#### Step 3: Filter Books
- Use the **category dropdown** to see only:
  - Fiction
  - Non-Fiction
  - Science
  - History
  - Technology

#### Step 4: View Book Details
1. Click on any book
2. You'll see:
   - Cover image
   - Title, Author, Publisher
   - Description
   - **Availability** (Available or Not Available)
   - Number of copies

#### Step 5: Borrow the Book
1. If it says "**Available**" (green badge)
2. Click "**Borrow This Book**" button
3. Confirm by clicking "OK"
4. Done! You borrowed the book for 14 days

#### Step 6: Track Your Loan
1. Go to "**My Account**" → "**My Loans**"
2. See your borrowed book with:
   - Loan Date (when you borrowed it)
   - Due Date (when to return it)
   - Status (Active, Returned, Overdue)

---

### **Reserving Books**

#### When to Reserve?
- When a book shows "**Not Available**" (red badge)
- All copies are currently borrowed

#### How to Reserve:
1. Click on the unavailable book
2. Click "**Reserve This Book**" button
3. Confirm by clicking "OK"
4. You're now in the waiting list!

#### What Happens Next?
1. Someone returns the book
2. You're next in line (first come, first served)
3. System notifies you
4. Book status changes to "Fulfilled"
5. You have **7 days** to borrow it
6. Go to the book and click "Borrow This Book"

#### Manage Your Reservations:
1. Go to "**My Account**" → "**My Reservations**"
2. See all your reservations
3. Click "**Cancel**" if you don't want the book anymore

---

### **Managing Your Profile**

#### View Your Profile:
1. Go to "**My Account**" → "**My Profile**"
2. See your personal information

#### Edit Your Profile:
1. Click "**Edit Profile**" button
2. Update:
   - First Name
   - Last Name
   - Address
   - Phone Number
3. Click "**Save Changes**"

**Note:** Email and Date of Birth cannot be changed.

---

### **Your Dashboard**

#### What is the Dashboard?
Your personal control center showing:
- 📊 **Active Loans**: How many books you have
- ⚠️ **Overdue Books**: Books you need to return ASAP
- 🔖 **Active Reservations**: Books you're waiting for

#### How to Access:
- Go to "**My Account**" → "**Dashboard**"

---

## ⚠️ Important Rules

### Loan Rules
- **Loan Period**: 14 days
- **Late Fee**: $1 per day after due date
- **Limit**: One active loan per book (can't borrow same book twice)

### Reservation Rules
- **Only for unavailable books**: Can't reserve available books
- **Pickup Window**: 7 days after book becomes available
- **Limit**: One reservation per book
- **Queue**: First come, first served

### Book Availability
- **Total Copies**: How many the library owns
- **Available Copies**: How many you can borrow right now
- Updates automatically when borrowed/returned

---

## 🎯 Common Scenarios

### Scenario 1: Borrowing Your First Book
```
1. Login to your account
2. Click "Books" in menu
3. Find "Harry Potter"
4. Click on it
5. See "Available" badge (green)
6. Click "Borrow This Book"
7. Confirm
8. Success! Due date: 14 days from now
9. Go to "My Loans" to see it
```

### Scenario 2: Reserving a Popular Book
```
1. Find "1984" in books
2. Click on it
3. See "Not Available" badge (red)
4. Click "Reserve This Book"
5. Confirm
6. Status: Pending
7. Wait for notification
8. When available: Status changes to Fulfilled
9. You have 7 days to borrow it
10. Go to "My Reservations" to track it
```

### Scenario 3: Returning a Book (Admin does this)
```
1. You finish reading the book
2. Return physical book to library
3. Admin/Librarian processes return in system
4. Your loan status changes to "Returned"
5. If late: Late fee is calculated automatically
6. Book becomes available for others
```

### Scenario 4: Editing Your Profile
```
1. Go to "My Account" → "My Profile"
2. Click "Edit Profile"
3. Change your address (e.g., moved to new house)
4. Change your phone number
5. Click "Save Changes"
6. Success message appears
7. Profile is updated
```

---

## 🆘 Troubleshooting

### "I can't borrow a book"
**Possible reasons:**
- ❌ Book is not available (all copies borrowed)
  - **Solution**: Reserve it instead
- ❌ You already have this book on loan
  - **Solution**: Return it first, then borrow again
- ❌ You're not logged in
  - **Solution**: Login to your account

### "I can't reserve a book"
**Possible reasons:**
- ❌ Book is available (has copies)
  - **Solution**: Borrow it instead of reserving
- ❌ You already have a reservation for this book
  - **Solution**: Wait for your current reservation
- ❌ You're not logged in
  - **Solution**: Login to your account

### "Where are my loans?"
- Go to "**My Account**" → "**My Loans**"
- You'll see ALL loans (current and past)

### "Where are my reservations?"
- Go to "**My Account**" → "**My Reservations**"
- You'll see ALL reservations with their status

### "How do I return a book?"
- You physically return the book to the library
- Admin/Librarian marks it as returned in the system
- You can't return books yourself online

### "I forgot when to return my book"
- Go to "**My Loans**"
- Look at the "**Due Date**" column
- If it says "OVERDUE" in red, return it ASAP!

---

## 📱 Navigation Guide

### Top Menu (When Logged In):
- **Home**: Main page
- **Books**: Browse all books
- **My Account** (dropdown):
  - Dashboard
  - My Loans
  - My Reservations
  - My Profile
- **Logout**: Sign out

### Quick Access Cards (Home Page):
- **Browse Books**: See all books
- **My Dashboard**: Your stats
- **My Loans**: Track borrowed books
- **My Reservations**: Manage waiting list

---

## ✨ Tips & Tricks

### 1. Use Search Effectively
- Search by title: "harry potter"
- Search by author: "rowling"
- Search by topic: "magic" or "science"

### 2. Filter by Category
- Want fiction? Select "Fiction" category
- Want science books? Select "Science" category
- Combine with search for better results

### 3. Check Due Dates Regularly
- Go to "My Loans" often
- Look for red "OVERDUE" badges
- Return books on time to avoid fees

### 4. Reserve Popular Books Early
- If a book is unavailable, reserve it immediately
- You'll be first in line when it's returned

### 5. Keep Profile Updated
- Update address if you move
- Update phone number for notifications
- Keep information current

---

## 🎉 Summary

**To Borrow:**
1. Find available book
2. Click "Borrow This Book"
3. Read for 14 days
4. Return on time

**To Reserve:**
1. Find unavailable book
2. Click "Reserve This Book"
3. Wait for notification
4. Borrow within 7 days

**To Track:**
- Loans: "My Account" → "My Loans"
- Reservations: "My Account" → "My Reservations"
- Profile: "My Account" → "My Profile"

**Remember:**
- 📅 14 days to read
- 💰 $1/day if late
- 🔖 Reserve if unavailable
- ⏰ 7 days to pick up reservations

---

## 🎓 You're Ready!

Now you know everything about using the Library Management System. Start browsing books and happy reading! 📚✨
