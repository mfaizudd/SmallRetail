using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallRetail.Web.Resources;

namespace SmallRetail.Web.Validators
{
    public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            RuleFor(t => t.TransactionProducts).Must(p => p.Count > 0).WithMessage("Must have at least one product");
        }
    }
}
