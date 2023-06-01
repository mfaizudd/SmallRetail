using System.Text.Json.Serialization;

namespace SmallRetail.WebApi.Data
{
    public class Transaction: BaseEntity
    {
        public long Id { get; set; }
        public required string UserId { get; set; }
        public required long ShopId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Shop? Shop { get; set; }
        
        public List<TransactionProduct> Products { get; set; } = new List<TransactionProduct>();
    }
}
