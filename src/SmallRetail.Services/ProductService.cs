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
            return _db.Products;
        }

        public Product Get(Guid id)
        {
            return _db.Products.Find(id);
        }

        public void Create(Product product)
        {
            _db.Add(product);
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            _db.Update(product);
            _db.SaveChanges();
        }

        public void Delete(Product product)
        {
            _db.Remove(product);
            _db.SaveChanges();
        }
    }
}
