using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();


            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<User>()
                .HasMany(c => c.Orders)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);


            modelBuilder.Entity<Order>()
                .HasMany(c => c.orderItems)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId);


            modelBuilder.Entity<Product>()
                .HasMany(c => c.orderItems)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
