namespace SmallRetail.WebApi.Services.DTO
{
    public class ProductInput
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required string Barcode { get; set; }
        public required int ShopId { get; set; }
    }
}
