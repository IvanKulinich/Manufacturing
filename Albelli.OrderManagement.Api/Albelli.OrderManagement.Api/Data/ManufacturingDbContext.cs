using Albelli.OrderManagement.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Albelli.OrderManagement.Api.Data
{
    public class ManufacturingDbContext : DbContext
    {
        public ManufacturingDbContext(DbContextOptions<ManufacturingDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderLines)
                .WithOne(ol => ol.Product);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderLines)
                .WithOne(ol => ol.Order);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Type = "PhotoBook",
                    Width = 19.0
                },
                new Product
                {
                    Id = 2,
                    Type = "Calendar",
                    Width = 10.0
                },
                new Product
                {
                    Id = 3,
                    Type = "Canvas",
                    Width = 16.0
                },
                new Product
                {
                    Id = 4,
                    Type = "Cards",
                    Width = 4.7
                },
                new Product
                {
                    Id = 5,
                    Type = "Mug",
                    Width = 94
                });

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    MinPackageWidth = 19
                });

            modelBuilder.Entity<OrderLine>().HasData(
                new OrderLine
                {
                    Id = 1,
                    OrderId = 1,
                    Quantity = 1,
                    ProductId = 1
                });
        }
    }
}
