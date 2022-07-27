using System;
using System.Collections.Generic;

namespace SmallRetail.Data.Models
{
    public class Transaction
    {
        public Guid? Id { get; set; }
        public List<TransactionProduct> TransactionProducts { get; set; } = new List<TransactionProduct>();
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}