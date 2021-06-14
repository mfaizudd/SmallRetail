using System;

namespace SmallRetail.Web.Resources
{
    public class TransactionProductResource
    {
        public Guid TransactionId { get; set; }
        public TransactionResource Transaction {get; set; }
        public Guid ProductId { get; set; }
        public ProductResource Product { get; set; }
        public int Quantity { get; set; }
    }
}