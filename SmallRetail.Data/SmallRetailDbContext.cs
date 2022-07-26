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
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Type)
                .HasDefaultValue(UserType.User);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var host = Environment.GetEnvironmentVariable("DB_HOST");
                var port = Environment.GetEnvironmentVariable("DB_PORT");
                var name = Environment.GetEnvironmentVariable("DB_NAME");
                var user = Environment.GetEnvironmentVariable("DB_USER");
                var pass = Environment.GetEnvironmentVariable("DB_PASS");
                var connectionString = $"Host={host};Port={port};Database={name};Username={user};Password={pass};";
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}
