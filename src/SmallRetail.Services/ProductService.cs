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
            product.DateCreated = DateTime.UtcNow;
            product.DateUpdated = DateTime.UtcNow;
            _db.Add(product);
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            var existingProduct = _db.Products.Find(product.Id);
            if (existingProduct == null)
            {
                throw new ArgumentException("Product doesn't exists");
            }

            existingProduct.Barcode = product.Barcode;
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.DateUpdated = DateTime.UtcNow;
            _db.Update(existingProduct);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                throw new ArgumentException("Product doesn't exists");
            }
            _db.Remove(product);
            _db.SaveChanges();
        }
    }
}
