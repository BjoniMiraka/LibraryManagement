# 📖 What is ISBN?

## Definition

**ISBN = International Standard Book Number**

A unique numeric commercial book identifier assigned to each edition and variation of a publication.

---

## 🎯 Purpose

Think of ISBN like a **barcode or serial number for books**:
- Every published book has a unique ISBN
- No two different books share the same ISBN
- Used worldwide by libraries, bookstores, and publishers
- Makes it easy to identify, find, and order specific books

---

## 📊 Two Types

### **ISBN-10** (Older Format - 10 digits)

**Format:** `X-XXX-XXXXX-X`

**Example:**
```
0-451-52493-4
```

**Breakdown:**
- **0** - Language/Country (0 or 1 = English)
- **451** - Publisher code
- **52493** - Book title code
- **4** - Check digit (for validation)

---

### **ISBN-13** (Current Standard - 13 digits)

**Format:** `XXX-X-XXX-XXXXX-X`

**Example:**
```
978-0-451-52493-5
```

**Breakdown:**
- **978** - Bookland prefix (always 978 or 979)
- **0** - Language/Country
- **451** - Publisher code
- **52493** - Book title code
- **5** - Check digit

---

## 🔍 Real Examples

### **"1984" by George Orwell:**
```
ISBN-13: 978-0-451-52493-5
ISBN-10: 0-451-52493-4
```

### **"Harry Potter and the Philosopher's Stone":**
```
ISBN-13: 978-0-439-70818-8
ISBN-10: 0-439-70818-X
```

### **"The Great Gatsby":**
```
ISBN-13: 978-0-7432-7356-5
ISBN-10: 0-7432-7356-8
```

---

## 🌍 Why ISBN is Important

### **For Libraries:**
- ✅ Catalog books accurately
- ✅ Track inventory
- ✅ Order specific editions
- ✅ Avoid confusion between different editions

### **For Bookstores:**
- ✅ Scan barcodes at checkout
- ✅ Track sales
- ✅ Order from distributors
- ✅ Manage inventory

### **For Publishers:**
- ✅ Identify their books
- ✅ Track distribution
- ✅ Manage royalties
- ✅ Prevent counterfeiting

### **For Readers:**
- ✅ Find exact edition they want
- ✅ Verify book authenticity
- ✅ Compare prices across stores
- ✅ Search online databases

---

## 🔢 ISBN Check Digit (Validation)

The last digit is a **check digit** calculated from the other digits to detect errors.

### **ISBN-10 Check Digit:**
```
Example: 0-451-52493-?

Calculation:
(0×10 + 4×9 + 5×8 + 1×7 + 5×6 + 2×5 + 4×4 + 9×3 + 3×2) mod 11 = 4

Check digit = 4
Final ISBN: 0-451-52493-4
```

### **ISBN-13 Check Digit:**
```
Example: 978-0-451-52493-?

Calculation:
(9×1 + 7×3 + 8×1 + 0×3 + 4×1 + 5×3 + 1×1 + 5×3 + 2×1 + 4×3 + 9×1 + 3×3) mod 10 = 5

Check digit = 5
Final ISBN: 978-0-451-52493-5
```

---

## 💻 ISBN in Our Library System

### **Where ISBN is Used:**

**In the Database:**
- **Table:** Books
- **Column:** ISBN (Text, 13 characters)
- **Example:** "978-0451524935"

**In the Code:**
- **Model:** `Models/Book.cs`
- **Validation:** `[Required]`, `[StringLength(13)]`
- **Custom Validator:** `Validators/ISBNAttribute.cs` (validates format and checksum)

**In the UI:**
- **Create Book Form:** Enter ISBN when adding new book
- **Book Details Page:** Display ISBN
- **Search:** Can search books by ISBN

### **ISBN Validation in Our App:**

Our custom `ISBNAttribute` validator checks:
1. ✅ Length is 10 or 13 digits
2. ✅ Contains only numbers (and 'X' for ISBN-10)
3. ✅ Check digit is correct (validates checksum)
4. ✅ Prevents invalid ISBNs

**Code Location:** `Validators/ISBNAttribute.cs`

---

## 🔍 How to Find a Book's ISBN

### **Physical Book:**
- Look on the back cover (near barcode)
- Check copyright page (inside front)
- Look at spine (sometimes printed there)

### **Online:**
- Amazon product page
- Google Books
- Publisher website
- Library catalog

---

## 📝 ISBN Format Rules

### **Valid ISBN-10:**
```
✅ 0451524934
✅ 0-451-52493-4
✅ 043970818X (X = 10)
```

### **Valid ISBN-13:**
```
✅ 9780451524935
✅ 978-0-451-52493-5
✅ 978-0451524935
```

### **Invalid:**
```
❌ 12345 (too short)
❌ 123456789012345 (too long)
❌ 978-0-451-52493-9 (wrong check digit)
❌ ABC-DEF-GHI-JK (contains letters other than X)
```

---

## 🌐 ISBN vs Other Identifiers

| Identifier | Used For | Example |
|------------|----------|---------|
| **ISBN** | Books | 978-0451524935 |
| **ISSN** | Magazines/Journals | 0028-0836 |
| **UPC** | Products | 012345678905 |
| **DOI** | Academic Papers | 10.1000/xyz123 |
| **ASIN** | Amazon Products | B000001234 |

---

## 🎓 Fun Facts

1. **ISBN started in 1970** - Before that, books had no standard identifier
2. **Over 150 million ISBNs** have been assigned worldwide
3. **Different editions = Different ISBNs** - Hardcover and paperback have different ISBNs
4. **E-books get ISBNs too** - Digital versions have their own ISBNs
5. **Self-published authors** can buy ISBNs for their books
6. **ISBN-13 became standard in 2007** - To accommodate more books
7. **The 'X' in ISBN-10** represents the number 10 (for check digit)

---

## 🔗 Useful Resources

- **ISBN International:** https://www.isbn-international.org/
- **ISBN Search:** https://isbnsearch.org/
- **Google Books:** https://books.google.com/
- **WorldCat:** https://www.worldcat.org/

---

## 💡 In Summary

**ISBN is like a Social Security Number for books:**
- ✅ Unique identifier
- ✅ Used worldwide
- ✅ Essential for libraries and bookstores
- ✅ Helps organize and find books
- ✅ Prevents confusion between editions

**In our Library Management System, ISBN helps:**
- Uniquely identify each book
- Prevent duplicate entries
- Search and find books quickly
- Validate book information
- Professional cataloging

---

**Now you know what ISBN is and why it's important in library systems!** 📚

