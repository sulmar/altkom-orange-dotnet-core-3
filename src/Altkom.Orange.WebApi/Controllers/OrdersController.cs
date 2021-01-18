using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.WebApi.Controllers
{
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        // zla praktyka GET api/orders?customerId=10

        // GET api/customers/{id}/orders

        [HttpGet("~/api/customers/{customerId}/orders")]
        public IActionResult GetOrders(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
