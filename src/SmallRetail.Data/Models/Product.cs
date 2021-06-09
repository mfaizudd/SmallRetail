using System;
using System.Collections.Generic;

namespace SmallRetail.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<TransactionProduct> TransactionProducts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}