using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            
            // Book - Category (One-to-Many)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book - Publisher (One-to-Many)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            // BookAuthor - Book (Many-to-One)
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // BookAuthor - Author (Many-to-One)
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Loan - Book (Many-to-One)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Loan - User (Many-to-One)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reservation - Book (Many-to-One)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reservation - User (Many-to-One)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Fiction", Description = "Fiction books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 2, Name = "Non-Fiction", Description = "Non-fiction books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 3, Name = "Science", Description = "Science books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 4, Name = "History", Description = "History books", CreatedAt = DateTime.UtcNow },
                new Category { CategoryId = 5, Name = "Technology", Description = "Technology books", CreatedAt = DateTime.UtcNow }
            );

            // Seed Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherId = 1, Name = "Penguin Books", Address = "New York, USA", Email = "info@penguin.com", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 2, Name = "HarperCollins", Address = "London, UK", Email = "info@harpercollins.com", CreatedAt = DateTime.UtcNow },
                new Publisher { PublisherId = 3, Name = "O'Reilly Media", Address = "California, USA", Email = "info@oreilly.com", CreatedAt = DateTime.UtcNow }
            );

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "George", LastName = "Orwell", Country = "UK", CreatedAt = DateTime.UtcNow },
                new Author { AuthorId = 2, FirstName = "Jane", LastName = "Austen", Country = "UK", CreatedAt = DateTime.UtcNow },
                new Author { AuthorId = 3, FirstName = "Stephen", LastName = "Hawking", Country = "UK", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}

