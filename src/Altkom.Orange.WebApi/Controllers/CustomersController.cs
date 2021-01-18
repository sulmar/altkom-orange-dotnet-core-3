using Altkom.Orange.IServices;
using Altkom.Orange.Models.SearchCriterias;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET api/customers
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var customers = customerService.Get();

        //    return Ok(customers);
        //}


        // bool IRouteConstraint.Match()

        // GET api/customers/{id}
        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            var customer = customerService.Get(id);

            return Ok(customer);
        }

        [HttpGet("{pesel:pesel}")]
        public IActionResult Get(string pesel)
        {
            var customer = customerService.Get(pesel);

            return Ok(customer);
        }

        // GET api/customers?FirstName=Neil&LastName=Effertz

        [HttpGet]
        public IActionResult Get(CustomerSearchCriteria searchCriteria)
        {
            var customers = customerService.Get(searchCriteria);

            return Ok(customers);
        }
    }
}
