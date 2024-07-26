using FluentValidation;
using Para.Data.Domain;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Api.Validation
{
    public class CustomerValidation: AbstractValidator<CustomerRequest>
    {
        public CustomerValidation()
        {
            //Veritabanındaki kurallar baz alınarak Customer için hazırlanmış FluenValidation tipindeki kurallar.

            RuleFor(customer => customer.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must be 50 characters or less.");

            RuleFor(customer => customer.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must be 50 characters or less.");

            RuleFor(customer => customer.IdentityNumber)
                .NotEmpty().WithMessage("Identity number is required.")
                .Length(11).WithMessage("Identity number must be 11 characters.");

            RuleFor(customer => customer.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email must be 100 characters or less.");

            RuleFor(customer => customer.CustomerNumber)
                .NotEmpty().WithMessage("Customer number is required.");

            RuleFor(customer => customer.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
        }
    }
}
