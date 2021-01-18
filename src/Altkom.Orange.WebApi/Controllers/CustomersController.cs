using Altkom.Orange.IServices;
using Altkom.Orange.Models.SearchCriterias;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.WebApi.Controllers
{
    [ApiController]
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
        public IActionResult Get(string pesel, [FromServices] IMessageService messageService)    // wstrzykiwanie poprzez metode
        {
            messageService.Send(pesel);

            var customer = customerService.Get(pesel);

            return Ok(customer);
        }

        // GET api/customers?FirstName=Neil&LastName=Effertz
        [HttpGet]
        public IActionResult Get([FromQuery] CustomerSearchCriteria searchCriteria)
        {
            var customers = customerService.Get(searchCriteria);

            return Ok(customers);
        }

        // GET api/customers/female
        [HttpGet("female")]
        public IActionResult GetFemales()
        {
            CustomerSearchCriteria searchCriteria = new CustomerSearchCriteria { Gender = Models.Gender.Female };

            var customers = customerService.Get(searchCriteria);

            return Ok(customers);
        }

       

    }
}
