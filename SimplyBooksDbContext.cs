using Microsoft.EntityFrameworkCore;
using SimplyBooks.Models;

namespace SimplyBooks.Data
{
  public class SimplyBooksDbContext : DbContext
  {
    public SimplyBooksDbContext(DbContextOptions<SimplyBooksDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Relationships
      modelBuilder.Entity<User>()
        .HasMany(u => u.Authors)
        .WithOne()
        .HasForeignKey(a => a.UserId)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<User>()
        .HasMany(u => u.Books)
        .WithOne()
        .HasForeignKey(b => b.UserId)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Author>()
        .HasMany(a => a.Books)
        .WithOne(b => b.Author)
        .HasForeignKey(b => b.AuthorId)
        .OnDelete(DeleteBehavior.Cascade);

      // Seed Data
      modelBuilder.Entity<User>().HasData(
        new User { Id = 1, UserName = "noahc", Email = "noah@example.com" },
        new User { Id = 2, UserName = "reader99", Email = "reader99@example.com" }
      );

      modelBuilder.Entity<Author>().HasData(
        new Author { Id = 1, FirstName = "Toni", LastName = "Morrison", Email = "toni@example.com", Image = "https://example.com/toni.jpg", Favorite = true, UserId = 1 },
        new Author { Id = 2, FirstName = "George", LastName = "Orwell", Email = "orwell@example.com", Image = "https://example.com/orwell.jpg", Favorite = false, UserId = 1 },
        new Author { Id = 3, FirstName = "Jane", LastName = "Austen", Email = "austen@example.com", Image = "https://example.com/jane.jpg", Favorite = true, UserId = 2 }
      );

      modelBuilder.Entity<Book>().HasData(
        new Book { Id = 1, Title = "Beloved", Description = "A novel about the legacy of slavery.", Image = "https://example.com/beloved.jpg", Price = 19.99m, Sale = false, AuthorId = 1, UserId = 1 },
        new Book { Id = 2, Title = "1984", Description = "A dystopian social science fiction novel.", Image = "https://example.com/1984.jpg", Price = 15.99m, Sale = true, AuthorId = 2, UserId = 1 },
        new Book { Id = 3, Title = "Pride and Prejudice", Description = "A romantic novel of manners.", Image = "https://example.com/pride.jpg", Price = 12.99m, Sale = false, AuthorId = 3, UserId = 2 }
      );
    }
  }
}
