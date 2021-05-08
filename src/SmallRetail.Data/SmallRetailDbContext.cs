using System;
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

        public virtual DbSet<Product> Products { get; set; }
    }
}
