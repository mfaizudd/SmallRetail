namespace SmallRetail.WebApi.Data
{
    public class TransactionProduct : BaseEntity
    {
        public long Id { get; set; }
        public required long TransactionId { get; set; }
        public required long ProductId { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
        public required decimal Paid { get; set; }

        public Transaction? Transaction { get; set; }
        public Product? Product { get; set; }
    }
}
