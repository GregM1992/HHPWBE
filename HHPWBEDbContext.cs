using Microsoft.EntityFrameworkCore;
using HHPWBE.Models;


public class HHPWBEDbContext : DbContext
{

    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<OrderType> OrderTypes { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }

    public HHPWBEDbContext(DbContextOptions<HHPWBEDbContext> context) : base(context)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderType>().HasData(new OrderType[]
        {
            new OrderType { Id = 1, Type = "Walk-In"},
            new OrderType { Id = 2, Type = "Call-In"}
        });
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, CustomerName = "John Man", CustomerPhone = "612-343-2345", CustomerEmail = "verygood@eng.com", OrderType = 1, DateCreated = new DateTime(2024,04,05), DateClosed = null, IsClosed = false },
            new Order { Id = 2, CustomerName = "Jeff Dude", CustomerPhone = "762-232-3234", CustomerEmail = "soCool@32.com", OrderType = 2, DateCreated = new DateTime(2024,04,04), DateClosed = new DateTime(2024,04,04),SubTotal = 20.00m, Tip = 5.00m, IsClosed = true },
        });
        modelBuilder.Entity<Item>().HasData(new Item[]
        {
            new Item { Id = 1, Name = "6pc Wings", Price = 6.00m},
            new Item { Id = 2, Name = "12pc Wings", Price = 10.00m},
            new Item { Id = 3, Name = "14in Cheese", Price = 13.00m},
            new Item { Id = 4, Name = "18in Cheese", Price = 15.00m},
            new Item { Id = 5, Name = "14in Pepperoni", Price = 15.00m},
            new Item { Id = 6, Name = "18in Pepperoni", Price = 20.00m}
        });
        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User { Id = 1, Uid = "fgjfqhqAvZNUIbI0Z3v9McdQxNy1"}
        });
    }
}

    
