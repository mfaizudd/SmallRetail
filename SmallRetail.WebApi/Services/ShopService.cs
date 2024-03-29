﻿using Microsoft.EntityFrameworkCore;
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
            var model = input.ToModel();
            await _db.Shops.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task Delete(long id)
        {
            var model = await _db.Shops.SingleAsync(x => x.Id == id);
            _db.Shops.Remove(model);
            await _db.SaveChangesAsync();
        }

        public async Task<Shop?> Get(long id)
        {
            var model = await _db.Shops.FindAsync(id);
            return model;
        }

        public async Task<List<Shop>> List(int limit = 10, int offset = 0)
        {
            return await _db.Shops.Skip(offset).Take(limit).OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Shop> Update(long id, ShopInput input)
        {
            var model = await _db.Shops.SingleAsync(x => x.Id == id);
            input.Apply(ref model);
            _db.Shops.Update(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<bool> AddProducts(long id, ShopProductInput input)
        {
            var existing = _db.ShopProducts
                .FirstOrDefault(x => x.ShopId == id && x.ProductId == input.ProductId);
            if (existing != null)
            {
                input.Stock += existing.Stock;
                input.Apply(ref existing);
                _db.ShopProducts.Update(existing);
            }
            else
            {
                var model = input.ToModel(id);
                await _db.ShopProducts.AddAsync(model);
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
