using SmallRetail.WebApi.Data;

namespace SmallRetail.WebApi.Services.DTO
{
    public class ShopProductInput
    {
        public long ProductId { get; set; }
        public long Stock { get; set; }

        public ShopProduct ToModel(long shopId) => new()
        {
            ProductId = ProductId,
            Stock = Stock,
            ShopId = shopId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
