using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallRetail.Web.Resources
{
    public class TransactionRequest
    {
        public List<TransactionProductRequest> TransactionProducts { get; set; }
    }
}
