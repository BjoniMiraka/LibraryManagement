# 📊 Database Tables - Complete Explanation

## Overview

This Library Management System has **13 tables total**:
- **8 Main Tables** - Your business logic (Books, Authors, etc.)
- **5 Identity Tables** - Microsoft's authentication system

---

## 🎯 Main Tables (8)

### 1. 📚 **Books Table**

**Purpose:** Store all book information in the library

**What it stores:**
- Book details (title, ISBN, description)
- Publication information (year, publisher)
- Inventory tracking (total copies, available copies)
- Cover image URL
- Links to category and publisher

**Columns:**
| Column | Type | Description | Example |
|--------|------|-------------|---------|
| BookId | Integer | Unique book ID (Primary Key) | 1 |
| Title | Text | Book title | "1984" |
| ISBN | Text | International Standard Book Number | "978-0451524935" |
| PublishedYear | Integer | Year book was published | 1949 |
| Description | Text | Book summary/description | "Dystopian novel..." |
| TotalCopies | Integer | How many copies library owns | 5 |
| AvailableCopies | Integer | How many are not loaned out | 3 |
| CoverImageUrl | Text | URL to book cover image | "https://..." |
| CategoryId | Integer | Foreign Key → Categories | 1 |
| PublisherId | Integer | Foreign Key → Publishers | 1 |
| CreatedAt | DateTime | When book was added | 2025-12-27 |
| UpdatedAt | DateTime | Last update time | 2025-12-28 |

**Why we need it:**
- Core entity of any library system
- Tracks what books you have
- Manages inventory (how many copies, how many available)
- Links to other tables (categories, publishers, authors)

**Real Example:**
```
BookId: 1
Title: "1984"
ISBN: "978-0451524935"
PublishedYear: 1949
TotalCopies: 5
AvailableCopies: 3 (2 are currently loaned out)
CategoryId: 1 (Fiction)
PublisherId: 1 (Penguin Books)
```

---

### 2. ✍️ **Authors Table**

**Purpose:** Store information about book authors

**What it stores:**
- Author personal information
- Biography
- Country of origin
- Birth date

**Columns:**
| Column | Type | Description | Example |
|--------|------|-------------|---------|
| AuthorId | Integer | Unique author ID (Primary Key) | 1 |
| FirstName | Text | Author's first name | "George" |
| LastName | Text | Author's last name | "Orwell" |
| Biography | Text | Author's life story | "English novelist..." |
| DateOfBirth | DateTime | Birth date | 1903-06-25 |
| Country | Text | Country of origin | "United Kingdom" |
| CreatedAt | DateTime | When added to system | 2025-12-27 |

**Why we need it:**
- Authors write multiple books
- Multiple books can share the same author
- Avoid repeating author info for each book
- Can search books by author
- Can display author information on book pages

**Real Example:**
```
AuthorId: 1
FirstName: "George"
LastName: "Orwell"
Country: "United Kingdom"
Biography: "English novelist, essayist, and critic..."
```

**Relationship:**
- One author can write MANY books
- One book can have MANY authors
- Connected via **BookAuthors** junction table

---

### 3. 🏷️ **Categories Table**

**Purpose:** Organize books into categories/genres

**What it stores:**
- Category names (Fiction, Science, History, etc.)
- Category descriptions

**Columns:**
| Column | Type | Description | Example |
|--------|------|-------------|---------|
| CategoryId | Integer | Unique category ID (Primary Key) | 1 |
| Name | Text | Category name | "Fiction" |
| Description | Text | What this category includes | "Fiction books" |
| CreatedAt | DateTime | When category was created | 2025-12-27 |

**Why we need it:**
- Organize library collection
- Users can browse by category
- Filter books by genre
- Easy to add new categories
- Professional library organization

**Pre-loaded Categories:**
```
1. Fiction - Fictional stories
2. Non-Fiction - Real-world topics
3. Science - Scientific books
4. History - Historical books
5. Technology - Tech-related books
```

**Relationship:**
- One category has MANY books
- Each book belongs to ONE category

---

### 4. 🏢 **Publishers Table**

**Purpose:** Store publisher company information

**What it stores:**
- Publisher company details
- Contact information
- Address and website

**Columns:**
| Column | Type | Description | Example |
|--------|------|-------------|---------|
| PublisherId | Integer | Unique publisher ID (Primary Key) | 1 |
| Name | Text | Publisher company name | "Penguin Books" |
| Address | Text | Publisher address | "New York, USA" |
| Phone | Text | Contact phone | "+1-555-0123" |
| Email | Text | Contact email | "info@penguin.com" |
| Website | Text | Publisher website | "www.penguin.com" |
| CreatedAt | DateTime | When added to system | 2025-12-27 |

**Why we need it:**
- Track which company published each book
- Store publisher contact information
- Can search books by publisher
- Professional library management
- Avoid repeating publisher info for each book

**Real Example:**
```
PublisherId: 1
Name: "Penguin Books"
Address: "New York, USA"
Email: "info@penguin.com"
```

**Relationship:**
- One publisher publishes MANY books
- Each book has ONE publisher

---

### 5. 🔗 **BookAuthors Table** (Junction Table)

**Purpose:** Link books to their authors (Many-to-Many relationship)

**What it stores:**
- Which books are written by which authors
- Allows multiple authors per book
- Allows one author to have multiple books

**Columns:**
| Column | Type | Description | Example |
|--------|------|-------------|---------|
| BookAuthorId | Integer | Unique ID (Primary Key) | 1 |
| BookId | Integer | Foreign Key → Books | 1 |
| AuthorId | Integer | Foreign Key → Authors | 1 |
| CreatedAt | DateTime | When link was created | 2025-12-27 |

**Why we need it:**
- **Solves Many-to-Many relationship problem**
- One book can have multiple authors (co-authors)
- One author can write multiple books
- Can't store this directly in Books or Authors table

**How it works:**
```
Books Table          BookAuthors Table        Authors Table
┌──────────┐        ┌─────────────────┐      ┌──────────────┐
│ BookId: 1│───────→│ BookId: 1       │─────→│ AuthorId: 1  │
│ "1984"   │        │ AuthorId: 1     │      │ "G. Orwell"  │
└──────────┘        └─────────────────┘      └──────────────┘
                    ┌─────────────────┐      
┌──────────┐        │ BookId: 2       │      
│ BookId: 2│───────→│ AuthorId: 1     │──────→ (Same author)
│"AnimalFrm"│       └─────────────────┘      
└──────────┘        
```

**Real Example:**
```
Entry 1: BookId=1, AuthorId=1 → "1984" by "George Orwell"
Entry 2: BookId=2, AuthorId=1 → "Animal Farm" by "George Orwell"
Entry 3: BookId=3, AuthorId=2 → "Pride and Prejudice" by "Jane Austen"
Entry 4: BookId=4, AuthorId=2 → Book with multiple authors
Entry 5: BookId=4, AuthorId=3 → Same book, different author
```

---

### 6. 📖 **Loans Table**

**Purpose:** Track which books are loaned to which users

**What it stores:**
- Who borrowed what book
- When they borrowed it
- When they should return it
- When they actually returned it
- Late fees if applicable

**Columns:**
| Column | Type | Description | Example |
|--------|------|-------------|---------|
| LoanId | Integer | Unique loan ID (Primary Key) | 1 |
| BookId | Integer | Foreign Key → Books | 1 |
| UserId | Text | Foreign Key → AspNetUsers | "abc123" |
| LoanDate | DateTime | When book was borrowed | 2025-12-20 |
| DueDate | DateTime | When book should be returned | 2026-01-03 |
| ReturnDate | DateTime | When book was actually returned | NULL (not yet) |
| Status | Enum | Active, Returned, Overdue, Lost | Active |
| LateFee | Decimal | Late fee amount if overdue | $5.00 |
| Notes | Text | Additional notes | "Damaged cover" |

**Why we need it:**
- Track who has which books
- Calculate due dates (14 days from loan)
- Calculate late fees ($1 per day overdue)
- View borrowing history
- Manage returns
- Track overdue books

**Real Example:**
```
LoanId: 1
BookId: 1 ("1984")
UserId: "abc123" (John Doe)
LoanDate: 2025-12-20
DueDate: 2026-01-03 (14 days later)
ReturnDate: NULL (not returned yet)
Status: Active
LateFee: NULL (not late yet)
```

**Status Types:**
- **Active** - Currently borrowed, not returned
- **Returned** - Book has been returned
- **Overdue** - Past due date, not returned
- **Lost** - Book reported as lost

**Relationship:**
- One book can have MANY loans (over time)
- One user can have MANY loans
- Each loan is for ONE book and ONE user

---

### 7. 🔖 **Reservations Table**

**Purpose:** Allow users to reserve books that are currently unavailable

**What it stores:**
- Who wants to reserve which book
- When reservation was made
- When reservation expires
- Reservation status

**Columns:**
| Column | Type | Description | Example |
|--------|------|-------------|---------|
| ReservationId | Integer | Unique reservation ID (Primary Key) | 1 |
| BookId | Integer | Foreign Key → Books | 1 |
| UserId | Text | Foreign Key → AspNetUsers | "xyz789" |
| ReservationDate | DateTime | When reservation was made | 2025-12-25 |
| ExpiryDate | DateTime | When reservation expires | 2026-01-01 |
| Status | Enum | Pending, Fulfilled, Cancelled, Expired | Pending |
| Notes | Text | Additional notes | "Notify by email" |

**Why we need it:**
- All copies of a book are loaned out
- User wants to be notified when available
- Fair queue system (first come, first served)
- Manage waiting list

**How it works:**
1. User wants "1984" but all 5 copies are loaned out
2. User creates a reservation
3. When someone returns "1984", system notifies user
4. User has 7 days to pick up the book
5. If not picked up, reservation expires

**Real Example:**
```
ReservationId: 1
BookId: 1 ("1984" - all copies loaned out)
UserId: "xyz789" (Jane Smith)
ReservationDate: 2025-12-25
ExpiryDate: 2026-01-01 (7 days to pick up)
Status: Pending (waiting for book)
```

**Status Types:**
- **Pending** - Waiting for book to become available
- **Fulfilled** - Book is available, user notified
- **Cancelled** - User cancelled reservation
- **Expired** - User didn't pick up in time

**Relationship:**
- One book can have MANY reservations
- One user can have MANY reservations
- Each reservation is for ONE book and ONE user

---

### 8. 👤 **AspNetUsers Table** (Extended Identity)

**Purpose:** Store user accounts with custom fields

**What it stores:**
- Login credentials (email, password)
- Personal information (name, address, birth date)
- Account settings
- Links to loans and reservations

**Columns:**
| Column | Type | Description | Example |
|--------|------|-------------|---------|
| Id | Text | Unique user ID (Primary Key) | "abc123..." |
| **FirstName** | Text | User's first name (CUSTOM) | "John" |
| **LastName** | Text | User's last name (CUSTOM) | "Doe" |
| **Address** | Text | User's address (CUSTOM) | "123 Main St" |
| **DateOfBirth** | DateTime | Birth date (CUSTOM) | 1990-01-15 |
| **CreatedAt** | DateTime | Account creation (CUSTOM) | 2025-12-27 |
| UserName | Text | Login username (Identity) | "john@example.com" |
| Email | Text | Email address (Identity) | "john@example.com" |
| EmailConfirmed | Boolean | Email verified? (Identity) | true |
| PasswordHash | Text | Encrypted password (Identity) | "$2a$11$..." |
| PhoneNumber | Text | Phone number (Identity) | "+1-555-0123" |
| SecurityStamp | Text | Security token (Identity) | "..." |
| LockoutEnabled | Boolean | Can be locked out? (Identity) | true |
| AccessFailedCount | Integer | Failed login attempts (Identity) | 0 |

**Why we need it:**
- Every user needs an account to login
- Track who borrows books
- Store personal information
- Role assignment (Admin, Librarian, Member)
- Authentication and authorization

**Real Example:**
```
Id: "abc123-def456-ghi789"
FirstName: "John"
LastName: "Doe"
Email: "john@example.com"
PasswordHash: "$2a$11$encrypted..." (secure, hashed)
DateOfBirth: 1990-01-15
Role: "Member"
```

**Custom Fields (Added by us):**
- FirstName, LastName - User's full name
- Address - Mailing address
- DateOfBirth - For age verification
- CreatedAt - When account was created

**Identity Fields (From Microsoft):**
- Email, PasswordHash - Login credentials
- PhoneNumber - Contact info
- SecurityStamp - Security features
- LockoutEnabled - Account security

**Relationship:**
- One user can have MANY loans
- One user can have MANY reservations

---

## 🔐 Identity Tables (5)

These tables are **automatically created by ASP.NET Core Identity** for authentication and authorization.

### 9. 🎭 **AspNetRoles Table**

**Purpose:** Define user roles in the system

**What it stores:**
- Role names (Admin, Librarian, Member)
- Role IDs

**Pre-loaded Roles:**
```
1. Admin - Full system access
2. Librarian - Manage books and loans
3. Member - Browse and borrow books
```

**Why we need it:**
- Role-based access control
- Different permissions for different users
- Admins can do everything
- Members can only browse and borrow

---

### 10. 🔗 **AspNetUserRoles Table**

**Purpose:** Link users to their roles (Many-to-Many)

**What it stores:**
- Which users have which roles
- One user can have multiple roles

**Example:**
```
UserId: "abc123", RoleId: "role-admin" → John is Admin
UserId: "xyz789", RoleId: "role-member" → Jane is Member
```

**Why we need it:**
- Assign roles to users
- Check user permissions
- One user can be both Admin and Librarian

---

### 11. 🏷️ **AspNetUserClaims Table**

**Purpose:** Store additional user permissions (claims)

**What it stores:**
- Fine-grained permissions
- Custom user attributes

**Example:**
```
UserId: "abc123", ClaimType: "CanDeleteBooks", ClaimValue: "true"
```

**Why we need it:**
- More detailed permissions than roles
- Custom user attributes

---

### 12. 🎫 **AspNetRoleClaims Table**

**Purpose:** Store permissions assigned to roles

**What it stores:**
- What each role can do
- Role-level permissions

**Example:**
```
RoleId: "role-admin", ClaimType: "CanManageUsers", ClaimValue: "true"
```

**Why we need it:**
- Define role capabilities
- Centralized permission management

---

### 13. 🔑 **AspNetUserTokens Table**

**Purpose:** Store security tokens for users

**What it stores:**
- Password reset tokens
- Email confirmation tokens
- Two-factor authentication tokens

**Why we need it:**
- Secure password reset
- Email verification
- Security features

---

## 📊 Table Relationships Diagram

```
┌─────────────────┐
│  AspNetUsers    │ (User accounts)
└────────┬────────┘
         │
    ┌────┴────┐
    ↓         ↓
┌──────┐  ┌──────────┐
│Loans │  │Reservations│
└──┬───┘  └────┬─────┘
   │           │
   └─────┬─────┘
         ↓
    ┌────────┐
    │ Books  │ (Main entity)
    └───┬────┘
        │
    ┌───┴────┬─────────┬──────────┐
    ↓        ↓         ↓          ↓
┌──────┐ ┌─────┐ ┌────────┐ ┌──────────┐
│Category│ │Publisher│ │BookAuthors│ │Authors   │
└──────┘ └─────┘ └────┬───┘ └──────────┘
                      │
                      └──────→ (Many-to-Many)
```

---

## 🎯 Why This Design?

### **1. No Data Duplication**
❌ Bad: Store author name in every book
```
Book 1: "1984", Author: "George Orwell"
Book 2: "Animal Farm", Author: "George Orwell" (duplicate!)
```

✅ Good: Separate Authors table
```
Author 1: "George Orwell"
Book 1: "1984", AuthorId: 1
Book 2: "Animal Farm", AuthorId: 1 (no duplication!)
```

### **2. Easy to Update**
- Change author name once → updates all their books
- Change category name once → updates all books in that category

### **3. Data Integrity**
- Can't delete an author if they have books
- Can't delete a book if it's currently loaned
- Foreign keys enforce valid relationships

### **4. Efficient Queries**
- Fast search by category
- Fast search by author
- Fast loan history lookup

---

## 📈 Summary

| Table | Records | Purpose | Type |
|-------|---------|---------|------|
| Books | Many | Store books | Main |
| Authors | Many | Store authors | Main |
| Categories | 5+ | Organize books | Main |
| Publishers | Many | Publisher info | Main |
| BookAuthors | Many | Link books & authors | Junction |
| Loans | Many | Track borrowing | Main |
| Reservations | Many | Track reservations | Main |
| AspNetUsers | Many | User accounts | Identity |
| AspNetRoles | 3 | User roles | Identity |
| AspNetUserRoles | Many | User-role links | Identity |
| AspNetUserClaims | Few | User permissions | Identity |
| AspNetRoleClaims | Few | Role permissions | Identity |
| AspNetUserTokens | Few | Security tokens | Identity |

**Total: 13 Tables**
- 8 Main business tables
- 5 Identity authentication tables

---

## 🔍 How to View Tables

### **In DataGrip/Rider:**
1. Connect to `library.db`
2. Expand database tree
3. See all 13 tables
4. Right-click table → View Data

### **In Command Line:**
```bash
sqlite3 library.db
.tables
SELECT * FROM Books;
SELECT * FROM Authors;
.quit
```

---

**This design follows database best practices and is used in real-world library systems!** 🎉

