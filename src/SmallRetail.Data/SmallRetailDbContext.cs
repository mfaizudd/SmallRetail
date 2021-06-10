using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmallRetail.Data.Models;

namespace SmallRetail.Data
{
    public class SmallRetailDbContext : DbContext
    {
        public SmallRetailDbContext()
        { }
        
        public SmallRetailDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionProduct>()
                .HasKey(tp => new {tp.TransactionId, tp.ProductId});
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .HasIndex(u => new {u.Username, u.Password})
                .IsUnique();

            var r = new Random();
            var products = Enumerable.Range(0, 10)
                .Select(x => new Product
                {
                    Id = Guid.NewGuid(),
                    Barcode = r.Next(10000000, 99999999).ToString(),
                    Name = r.Next(10000000, 99999999).ToString(),
                    Price = r.Next(1, 999) * 100,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                });
            modelBuilder.Entity<Product>()
                .HasData(products);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
