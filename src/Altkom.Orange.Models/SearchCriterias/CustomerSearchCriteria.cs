using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.Orange.Models.SearchCriterias
{
    public abstract class SearchCriteria
    {

    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public Gender? Gender { get; set; }
    }
}
