using System;

namespace SmallRetail.WebApi.Controllers.Resources
{
    public class ProductRequest
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string Barcode { get; set; }
    }
}
