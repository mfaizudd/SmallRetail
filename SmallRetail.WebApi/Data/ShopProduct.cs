namespace SmallRetail.WebApi.Data
{
    public class ShopProduct : BaseEntity
    {
        public long Id { get; set; }
        public required long ShopId { get; set; }
        public required long ProductId { get; set; }
        public required long Stock { get; set; }
        public Shop? Shop { get; set; }
        public Product? Product { get; set; }
    }
}
