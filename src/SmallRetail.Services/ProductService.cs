using System;
using System.Collections.Generic;
using System.Linq;
using SmallRetail.Data;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public class ProductService : IProductService
    {
        private readonly SmallRetailDbContext _db;

        public ProductService(SmallRetailDbContext db)
        {
            _db = db;
        }
        
        public IEnumerable<Product> GetAll()
        {
            return new List<Product>()
            {
                new() {Barcode = "12423", Id = Guid.NewGuid().ToString(), Name = "Jbir", Price = 5000m },
                new() {Barcode = "12423", Id = Guid.NewGuid().ToString(), Name = "Jbir", Price = 5000m },
                new() {Barcode = "12423", Id = Guid.NewGuid().ToString(), Name = "Jbir", Price = 5000m },
            };
        }

        public Product Get()
        {
            throw new NotImplementedException();
        }

        public void Create(Product product)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
