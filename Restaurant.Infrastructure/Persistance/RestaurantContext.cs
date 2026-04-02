using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Persistance
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(m => m.CategoryId);

            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(m => m.CategoryId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(x => new { x.OrderId, x.MenuItemId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.MenuItem)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.MenuItemId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Table)
                .WithMany(x => x.TableOrders)
                .HasForeignKey(x =>x.TableId);

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Table)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.TableId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
