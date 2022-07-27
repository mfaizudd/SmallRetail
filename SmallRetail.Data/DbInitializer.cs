using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmallRetail.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallRetail.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<SmallRetailDbContext>();
            context?.Database.Migrate();
        }

        public void SeedData()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<SmallRetailDbContext>();
            if (context is null) return;

            // add fake products
            if (!context.Products.Any())
            {


                var productFaker = new Faker<Product>()
                    .RuleFor(p => p.Id, _ => Guid.NewGuid())
                    .RuleFor(p => p.Barcode, f => f.Random.ReplaceNumbers("#########"))
                    .RuleFor(p => p.Name, f => f.Lorem.Word())
                    .RuleFor(p => p.Price, f => f.Random.Number(99) * 100)
                    .RuleFor(p => p.DateCreated, f => f.Date.Past(3).ToUniversalTime())
                    .RuleFor(p => p.DateUpdated, f => f.Date.Past(2).ToUniversalTime());
                var products = Enumerable.Range(0, 10)
                    .Select(_ => productFaker.Generate());
                context.Products.AddRange(products);
            }

            context.SaveChanges();
        }
    }
}
