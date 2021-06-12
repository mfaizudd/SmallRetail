using System;
using System.Collections.Generic;

namespace SmallRetail.Web.Resources
{
    public class TransactionResource
    {
        public Guid Id { get; set; }
        public List<TransactionProductResource> TransactionProducts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}