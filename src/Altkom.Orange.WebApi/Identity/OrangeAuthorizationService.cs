using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.WebApi.Identity
{
    public class OrangeAuthorizationService : IAuthorizationService
    {
        private readonly ICustomerService customerService;

        public OrangeAuthorizationService(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public bool TryAuthorize(string username, string password, out Customer customer)
        {
            customer = customerService.GetByUserName(username);

            if (customer!=null)
            {
                return customer.HashedPassword == password;
            }

            return false;
        }
    }
}
