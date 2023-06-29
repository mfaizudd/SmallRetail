using SmallRetail.WebApi.Data;
using SmallRetail.WebApi.Services.DTO;

namespace SmallRetail.WebApi.Services
{
    public interface IProductService
    {
        Task<List<Product>> List(int limit = 10, int offset = 0);
        Task<Product?> Get(long id);
        Task<Product> Create(ProductInput input);
        Task<Product> Update(long id, ProductInput input);
        Task Delete(long id);
    }
}
