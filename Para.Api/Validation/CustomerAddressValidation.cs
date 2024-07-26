﻿using FluentValidation;
using Para.Data.Domain;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Api.Validation
{
    public class CustomerAddressValidation: AbstractValidator<CustomerAddressRequest>
    {
        public CustomerAddressValidation()
        {
            //Veritabanındaki kurallar baz alınarak CustomerAddress için hazırlanmış FluenValidation tipindeki kurallar.
            RuleFor(address => address.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country must be 50 characters or less.");

            RuleFor(address => address.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must be 50 characters or less.");

            RuleFor(address => address.AddressLine)
                .NotEmpty().WithMessage("Address line is required.")
                .MaximumLength(250).WithMessage("Address line must be 250 characters or less.");

            RuleFor(address => address.ZipCode)
                .Length(6).WithMessage("Zip code must be 6 characters.");

            RuleFor(address => address.IsDefault)
                .NotNull().WithMessage("Is active status is required.");
        }
    }
}
