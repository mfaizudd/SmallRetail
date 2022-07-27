using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallRetail.Web.Resources;

namespace SmallRetail.Web.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator(bool isUpdate = false)
        {
            RuleFor(u => u.Name).NotEmpty();
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
            When(u => !isUpdate || !string.IsNullOrEmpty(u.Password), () =>
            {
                RuleFor(u => u.Password)
                    .Must(x => x.Length > 8).WithMessage("Password length must be at least 8 characters long")
                    .Matches("[A-Z]+").WithMessage("Password must contains at least a upper case character")
                    .Matches("[a-z]+").WithMessage("Password must contains at least one lower case character")
                    .Matches("[0-9]+").WithMessage("Password must contains at least one 0-9");
            });
        }
    }
}
