using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallRetail.Data
{
    public class Shop
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Name { get; set; }
    }
}
