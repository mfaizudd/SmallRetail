using Microsoft.EntityFrameworkCore;
using SmallRetail.WebApi.Controllers.Resources;
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
            var product = input.ToModel();
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task Delete(long id)
        {
            var product = await _db.Products.SingleAsync(x => x.Id == id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product?> Get(long id)
        {
            var product = await _db.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<ListOutput<Product>> List(int limit = 10, int offset = 0, ProductFilter? filter = null)
        {
            var query = _db.Products.AsQueryable();
            if (filter?.Name != null)
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            if (filter?.Price != null)
            {
                query = query.Where(x => x.Price == filter.Price);
            }
            if (filter?.Barcode != null)
            {
                query = query.Where(x => x.Barcode == filter.Barcode);
            }
            if (filter?.UserId != null)
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }
            var result = new ListOutput<Product>
            {
                Items = await query.Skip(offset).Take(limit).ToListAsync(),
                Total = await query.CountAsync(),
            };
            return result;
        }

        public async Task<Product> Update(long id, ProductInput input)
        {
            var product = await _db.Products.SingleAsync(x => x.Id == id);
            input.Apply(ref product);
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<int> Count()
        {
            var count = await _db.Products.CountAsync();
            return count;
        }
    }
}
