using SmallRetail.WebApi.Data;
using SmallRetail.WebApi.Services.DTO;

namespace SmallRetail.WebApi.Services
{
    public interface IShopService
    {
        Task<List<Shop>> List(int limit = 10, int offset = 0);
        Task<Shop?> Get(long id);
        Task<Shop> Create(ShopInput input);
        Task<Shop> Update(long id, ShopInput input);
        Task Delete(long id);
        Task<bool> AddProducts(long id, ShopProductInput input);
    }
}
