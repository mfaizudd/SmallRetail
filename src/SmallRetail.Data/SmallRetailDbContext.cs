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
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }
    }
}
