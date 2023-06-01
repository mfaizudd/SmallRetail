using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallRetail.WebApi.Data
{
    public class Shop : BaseEntity
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
