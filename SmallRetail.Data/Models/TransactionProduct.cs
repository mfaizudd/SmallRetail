using System;

namespace SmallRetail.Data.Models
{
    public class TransactionProduct
    {
        public Guid? TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }

        public int? Quantity { get; set; }
    }
}