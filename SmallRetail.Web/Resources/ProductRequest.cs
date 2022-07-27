using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallRetail.Web.Resources
{
    public class ProductRequest
    {
        public string Barcode { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}
