using Altkom.Orange.IServices;
using FluentValidation;
using System;

namespace Altkom.Orange.Models.Validators
{

    // dotnet add package FluentValidation

    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly ICustomerService customerService;

        public CustomerValidator(ICustomerService customerService)
        {
            this.customerService = customerService;

            RuleFor(p => p.LastName).NotEmpty().Length(3, 50);
            RuleFor(p => p.Email).EmailAddress().NotEmpty();
            RuleFor(p => p.CreditAmount).InclusiveBetween(1, 1000);
            RuleFor(p => p.Pesel)
                .Must(ValidateChecksum).WithMessage(customer => $"Niepoprawny PESEL {customer.Pesel}")
                .Must(ValidateExists).WithMessage("Użytkownik o podanym numerze PESEL już istnieje");
            RuleFor(p => p.CreditAmount).NotEqual(0).When(p => !p.IsRemoved);
          //  RuleFor(p => p).Custom((c, context)=> context.)
        }


        private bool ValidateChecksum(string pesel)
        {
            return pesel.Length == 11;
        }

        private bool ValidateExists(string pesel)
        {
            Customer customer = customerService.Get(pesel);

            if (customer != null)
                return false;

            return true;
        }

    }
}
