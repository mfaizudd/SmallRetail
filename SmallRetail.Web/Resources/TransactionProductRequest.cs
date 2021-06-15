using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallRetail.Web.Resources
{
    public class TransactionProductRequest
    {
        public Guid TransactionId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
