using SmallRetail.WebApi.Data;

namespace SmallRetail.WebApi.Services.DTO
{
    public class ProductInput
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string Barcode { get; set; }
        public required string UserId { get; set; }

        public void Apply(ref Product product)
        {
            product.Name = Name;
            product.Price = Price;
            product.Barcode = Barcode;
            product.UserId = UserId;
            product.UpdatedAt = DateTime.UtcNow;
        }

        public Product ToModel() => new()
        {
            Name = Name,
            Price = Price,
            Barcode = Barcode,
            UserId = UserId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
