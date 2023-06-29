using SmallRetail.WebApi.Data;

namespace SmallRetail.WebApi.Services.DTO
{
    public class ShopInput
    {
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public void Apply(ref Shop shop)
        {
            shop.UserId = UserId;
            shop.Name = Name;
            shop.UpdatedAt = DateTime.UtcNow;
        }

        public Shop ToModel() => new()
        {
            UserId = UserId,
            Name = Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }
}
