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

            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, _ => Guid.NewGuid())
                .RuleFor(p => p.Barcode, f => f.Random.ReplaceNumbers("#########"))
                .RuleFor(p => p.Name, f => f.Lorem.Word())
                .RuleFor(p => p.Price, f => f.Random.Number(99) * 100)
                .RuleFor(p => p.DateCreated, f => f.Date.Past(3))
                .RuleFor(p => p.DateUpdated, f => f.Date.Past(2));
            var products = Enumerable.Range(0, 10)
                .Select(_ => productFaker.Generate());
            modelBuilder.Entity<Product>()
                .HasData(products);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DotNetEnv.Env.Load();
                var host = DotNetEnv.Env.GetString("DB_HOST");
                var port = DotNetEnv.Env.GetString("DB_PORT");
                var name = DotNetEnv.Env.GetString("DB_NAME");
                var user = DotNetEnv.Env.GetString("DB_USER");
                var pass = DotNetEnv.Env.GetString("DB_PASS");
                var connectionString = $"Host={host};Port={port};Database={name};Username={user};Password={pass};";
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}
