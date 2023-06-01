using System.Text.Json.Serialization;

namespace SmallRetail.WebApi.Data
{
    public class ShopEmployee : BaseEntity
    {
        public long Id { get; set; }
        public required long ShopId { get; set; }
        public required string UserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Shop? Shop { get; set; }
    }
}
