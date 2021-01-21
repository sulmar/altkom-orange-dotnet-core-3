using Altkom.Orange.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Orange.IServices
{
    public interface ICustomerClient
    {
        Task YouHaveGotNewCustomer(Customer customer);
        Task Pong();
    }
}
