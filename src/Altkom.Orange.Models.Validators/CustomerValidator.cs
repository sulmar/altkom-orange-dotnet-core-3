using FluentValidation;
using System;

namespace Altkom.Orange.Models.Validators
{

    // dotnet add package FluentValidation

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.LastName).NotEmpty().Length(3, 50);
            RuleFor(p => p.Email).EmailAddress().NotEmpty();
            RuleFor(p => p.CreditAmount).InclusiveBetween(1, 1000);
            RuleFor(p => p.Pesel).Must(Validate);
            RuleFor(p => p.CreditAmount).NotEqual(0).When(p => !p.IsRemoved);
          //  RuleFor(p => p).Custom((c, context)=> context.)
        }
        
        private bool Validate(string pesel)
        {
            return pesel.Length == 11;
        }

    }
}
