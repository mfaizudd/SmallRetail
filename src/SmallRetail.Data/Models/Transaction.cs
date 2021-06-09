using System;
using System.Collections.Generic;

namespace SmallRetail.Data.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public List<Product> TransactionProducts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}