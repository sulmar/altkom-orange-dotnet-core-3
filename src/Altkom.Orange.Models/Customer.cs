using System;
using System.ComponentModel.DataAnnotations;

namespace Altkom.Orange.Models
{

    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        [Range(1, 1000)]
        public decimal? CreditAmount { get; set; }
        public string Pesel { get; set; }
        public CustomerType CustomerType { get; set; }

    }

}
