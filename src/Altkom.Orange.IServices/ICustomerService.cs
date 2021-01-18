using Altkom.Orange.Models;
using Altkom.Orange.Models.SearchCriterias;
using System;
using System.Collections.Generic;

namespace Altkom.Orange.IServices
{
    public interface ICustomerService : IEntityService<Customer>
    {
        Customer Get(string pesel);
        IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);
    }
}
