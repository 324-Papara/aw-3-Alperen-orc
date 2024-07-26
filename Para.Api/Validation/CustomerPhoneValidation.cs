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
    public class CustomerPhoneValidation: AbstractValidator<CustomerPhoneRequest>
    {
        public CustomerPhoneValidation()
        {
            //Veritabanındaki kurallar baz alınarak CustomerPhone için hazırlanmış FluenValidation tipindeki kurallar.

            RuleFor(phone => phone.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");

            RuleFor(phone => phone.CountyCode)
                .NotEmpty().WithMessage("County code is required.")
                .Length(3).WithMessage("County code must be exactly 3 characters.");

            RuleFor(phone => phone.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Length(10).WithMessage("Phone number must be exactly 10 characters.");

            RuleFor(phone => phone.IsDefault)
                .NotNull().WithMessage("Is default status is required.");
        }
    }
}
