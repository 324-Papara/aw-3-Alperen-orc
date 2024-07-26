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
    public class CustomerDetailValidation: AbstractValidator<CustomerDetailRequest>
    {
        public CustomerDetailValidation()
        {
            //Veritabanındaki kurallar baz alınarak CustomerDetail için hazırlanmış FluenValidation tipindeki kurallar.

            RuleFor(detail => detail.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");

            RuleFor(detail => detail.FatherName)
                .NotEmpty().WithMessage("Father name is required.")
                .MaximumLength(50).WithMessage("Father name must be 50 characters or less.");

            RuleFor(detail => detail.MotherName)
                .NotEmpty().WithMessage("Mother name is required.")
                .MaximumLength(50).WithMessage("Mother name must be 50 characters or less.");

            RuleFor(detail => detail.EducationStatus)
                .NotEmpty().WithMessage("Education status is required.")
                .MaximumLength(50).WithMessage("Education status must be 50 characters or less.");

            RuleFor(detail => detail.MontlyIncome)
                .NotEmpty().WithMessage("Monthly income is required.")
                .MaximumLength(50).WithMessage("Monthly income must be 50 characters or less.");

            RuleFor(detail => detail.Occupation)
                .NotEmpty().WithMessage("Occupation is required.")
                .MaximumLength(50).WithMessage("Occupation must be 50 characters or less.");

        }
    }
}
