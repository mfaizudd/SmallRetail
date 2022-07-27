using System;

namespace SmallRetail.Web.Resources
{
    public class TransactionProductResponse
    {
        public Guid TransactionId { get; set; }
        public Guid ProductId { get; set; }
        public ProductResponse? Product { get; set; }
        public int Quantity { get; set; }
    }
}