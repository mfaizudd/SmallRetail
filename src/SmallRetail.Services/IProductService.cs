using System;
using System.Collections.Generic;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetAll();
        public Product Get(Guid id);
        public void Create(Product product);
        public void Update(Product product);
        public void Delete(Guid id);
    }
}