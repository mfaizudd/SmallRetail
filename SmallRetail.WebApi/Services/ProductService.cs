using Microsoft.EntityFrameworkCore;
using SmallRetail.WebApi.Data;
using SmallRetail.WebApi.Services.DTO;

namespace SmallRetail.WebApi.Services
{
    public class ProductService : IProductService
    {
        readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Product> Create(ProductInput input)
        {
            var product = new Product
            {
                Barcode = input.Barcode,
                Name = input.Name,
                Price = input.Price,
                ShopId = input.ShopId,
                Stock = input.Stock,
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task Delete(long id)
        {
            var product = await _db.Products.SingleAsync(x => x.Id == id);
            _db.Products.Remove(product);
        }

        public async Task<Product?> Get(long id)
        {
            var product = await _db.Products
                .Include(x => x.Shop)
                .FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<List<Product>> List(int limit = 10, int offset = 0)
        {
            return await _db.Products.Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<Product> Update(long id, ProductInput input)
        {
            var product = await _db.Products.SingleAsync(x => x.Id == id);
            product.Name = input.Name;
            product.Barcode = input.Barcode;
            product.Price = input.Price;
            product.ShopId = input.ShopId;
            product.Stock = input.Stock;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}
