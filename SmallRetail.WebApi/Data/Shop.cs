using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallRetail.WebApi.Data
{
    public class Shop : BaseEntity
    {
        public long Id { get; set; }
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public string? InviteCode { get; set; }
        public List<ShopProduct> Products { get; set; } = new();
        public List<ShopEmployee> Employees { get; set; } = new();
    }
}
