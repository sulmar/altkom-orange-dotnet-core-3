using System;

namespace Altkom.Orange.Models
{

    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public decimal? CreditAmount { get; set; }
        public string Pesel { get; set; }
        public CustomerType CustomerType { get; set; }

        public string UserName { get; set; }
        public string HashedPassword { get; set; }

    }

}
