using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Infrastructure.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Unit).IsRequired();
                entity.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
            });

            // Category configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
            });

            // Recipe configuration
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Unit).IsRequired();
                entity.Property(e => e.Instructions).HasMaxLength(200);
                entity.HasOne(e => e.Product).WithMany(e => e.Recipes).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Ingredient).WithMany().HasForeignKey(e => e.IngredientId).OnDelete(DeleteBehavior.Restrict);
            });

            // Stock configuration
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Unit).IsRequired();
                entity.Property(e => e.MinimumStock).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Location);
                entity.Property(e => e.Cost).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Product).WithMany(e => e.Stocks).HasForeignKey(e => e.ProductId);
            });

            // Table configuration
            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TableNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Status);
                entity.Property(e => e.Location);
            });

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderNumber).IsRequired();
                entity.Property(e => e.Status);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.FinalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Currency);
                entity.Property(e => e.Notes).HasMaxLength(500);
                entity.HasOne(e => e.Table).WithMany(e => e.Orders).HasForeignKey(e => e.TableId);
                entity.HasOne(e => e.Customer).WithMany(e => e.Orders).HasForeignKey(e => e.CustomerId);
                entity.HasOne(e => e.Employee).WithMany(e => e.Orders).HasForeignKey(e => e.EmployeeId);
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).HasColumnType("decimal(18,2)");
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SpecialInstructions).HasMaxLength(200);
                entity.Property(e => e.Status);
                entity.HasOne(e => e.Order).WithMany(e => e.OrderItems).HasForeignKey(e => e.OrderId);
                entity.HasOne(e => e.Product).WithMany(e => e.OrderItems).HasForeignKey(e => e.ProductId);
            });

            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.CustomerType);
                entity.Property(e => e.TotalSpent).HasColumnType("decimal(18,2)");
            });

            // Employee configuration
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Position).IsRequired();
                entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Department);
            });

            // Reservation configuration
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CustomerPhone).HasMaxLength(20);
                entity.Property(e => e.ReservationDate).IsRequired();
                entity.Property(e => e.ReservationTime).IsRequired();
                entity.Property(e => e.Status);
                entity.Property(e => e.SpecialRequests).HasMaxLength(500);
                entity.HasOne(e => e.Table).WithMany(e => e.Reservations).HasForeignKey(e => e.TableId);
                entity.HasOne(e => e.Customer).WithMany(e => e.Reservations).HasForeignKey(e => e.CustomerId);
            });

            // Payment configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaymentMethod).IsRequired();
                entity.Property(e => e.Currency);
                entity.Property(e => e.TransactionId).HasMaxLength(100);
                entity.Property(e => e.Status);
                entity.Property(e => e.Notes).HasMaxLength(500);
                entity.HasOne(e => e.Order).WithMany(e => e.Payments).HasForeignKey(e => e.OrderId);
            });
        }
    }
} 