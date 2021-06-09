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
            return null;
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
