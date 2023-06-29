using SmallRetail.WebApi.Controllers.Resources;
using SmallRetail.WebApi.Data;
using SmallRetail.WebApi.Services.DTO;

namespace SmallRetail.WebApi.Services
{
    public interface IProductService
    {
        Task<ListOutput<Product>> List(int limit = 10, int offset = 0, ProductFilter? filter = null);
        Task<int> Count();
        Task<Product?> Get(long id);
        Task<Product> Create(ProductInput input);
        Task<Product> Update(long id, ProductInput input);
        Task Delete(long id);
    }
}
