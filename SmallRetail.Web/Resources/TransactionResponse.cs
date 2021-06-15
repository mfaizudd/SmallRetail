using System;
using System.Collections.Generic;

namespace SmallRetail.Web.Resources
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public List<TransactionProductResponse> TransactionProducts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}