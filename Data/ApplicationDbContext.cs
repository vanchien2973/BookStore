using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Data;

public class ApplicationDbContext : IdentityDbContext<DefaultUser>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<BookStore.Models.Book> Book { get; set; } = default!;
    public DbSet<BookStore.Models.Category> Category { get; set; } = default!;
    public DbSet<BookStore.Models.CartItem> CartItems { get; set; } = default!;
    public DbSet<BookStore.Models.OrderItem> OrderItems { get; set; } = default!;
    public DbSet<BookStore.Models.Order> Orders { get; set; } = default!;
}