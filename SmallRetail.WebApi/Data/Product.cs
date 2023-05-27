using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmallRetail.WebApi.Data
{
    public class Product
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required string Barcode { get; set; }
        public required int ShopId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Shop? Shop { get; set; }
    }
}
