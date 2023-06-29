using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallRetail.WebApi.Data;

namespace SmallRetail.WebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public required DbSet<Shop> Shops { get; set; }
        public required DbSet<Product> Products { get; set; }
        public required DbSet<Transaction> Transactions { get; set; }
        public required DbSet<TransactionProduct> TransactionProducts { get; set; }
        public required DbSet<ShopProduct> ShopProducts { get; set; }
    }
}
