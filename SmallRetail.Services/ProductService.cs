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

        public IEnumerable<Product> GetAll(int limit = 10, int page = 1)
        {
            var query = _db.Products.OrderBy(p => p.Name);

            if (limit == 0)
                return query;

            return query
                .Skip(limit * (page - 1))
                .Take(limit);
        }

        public Product Get(params object[] keyValues)
        {
            return _db.Products.Find(keyValues);
        }

        public Product Find(Func<Product, bool> predicate)
        {
            return _db.Products.FirstOrDefault(predicate);
        }

        public IEnumerable<Product> Where(Func<Product, bool> predicate)
        {
            return _db.Products.Where(predicate);
        }

        public void Create(Product product)
        {
            product.DateCreated = DateTime.UtcNow;
            product.DateUpdated = DateTime.UtcNow;
            _db.Add(product);
            _db.SaveChanges();
        }

        public void Update(Product product, params object[] keyValues)
        {
            if (keyValues == null)
                throw new ArgumentNullException(nameof(keyValues));
            if (keyValues.Length == 0)
                throw new ArgumentException("Key isn't specified", nameof(keyValues));

            var existingProduct = _db.Products.Find(keyValues);
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

        public void Delete(params object[] keyValues)
        {
            var product = _db.Products.Find(keyValues);
            if (product == null)
            {
                throw new ArgumentException("Product doesn't exists");
            }
            _db.Remove(product);
            _db.SaveChanges();
        }
    }
}
