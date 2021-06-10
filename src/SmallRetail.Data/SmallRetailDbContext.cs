using System;
using System.Linq;
using Bogus;
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

            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Barcode, f => f.Random.ReplaceNumbers("#########"))
                .RuleFor(p => p.Name, f => f.Lorem.Word())
                .RuleFor(p => p.Price, f => f.Random.Number(99) * 100)
                .RuleFor(p => p.DateCreated, f => f.Date.Past(3))
                .RuleFor(p => p.DateUpdated, f => f.Date.Past(2));
            var products = Enumerable.Range(0, 10)
                .Select(x => productFaker.Generate());
            modelBuilder.Entity<Product>()
                .HasData(products);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
