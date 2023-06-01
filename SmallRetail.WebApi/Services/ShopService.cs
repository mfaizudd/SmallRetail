using Microsoft.EntityFrameworkCore;
using SmallRetail.WebApi.Data;
using SmallRetail.WebApi.Services.DTO;

namespace SmallRetail.WebApi.Services
{
    public class ShopService : IShopService
    {
        readonly AppDbContext _db;

        public ShopService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Shop> Create(ShopInput input)
        {
            var product = new Shop
            {
                Name = input.Name,
                UserId = input.UserId,
            };
            await _db.Shops.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task Delete(long id)
        {
            var product = await _db.Shops.SingleAsync(x => x.Id == id);
            _db.Shops.Remove(product);
        }

        public async Task<Shop?> Get(long id)
        {
            var product = await _db.Shops.FindAsync(id);
            return product;
        }

        public async Task<List<Shop>> List(int limit = 10, int offset = 0)
        {
            return await _db.Shops.Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<Shop> Update(long id, ShopInput input)
        {
            var product = await _db.Shops.SingleAsync(x => x.Id == id);
            product.Name = input.Name;
            product.UserId = input.UserId;
            product.UpdatedAt = DateTime.UtcNow;
            _db.Shops.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}
