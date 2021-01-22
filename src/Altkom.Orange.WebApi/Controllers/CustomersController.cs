using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Altkom.Orange.Models.SearchCriterias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.WebApi.Controllers
{
    /// <summary>
    /// Klienci
    /// </summary>
    [Authorize]
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


        /// <summary>
        /// Pobranie klienta
        /// </summary>
        /// <param name="id">Identyfikator</param>
        /// <returns>Klient</returns>
        [HttpGet("{id:int:min(1)}", Name = "GetCustomerById")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Customer> Get(int id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("{pesel:pesel}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string pesel, [FromServices] IMessageService messageService)    // wstrzykiwanie poprzez metode
        {
            messageService.Send(pesel);

            var customer = customerService.Get(pesel);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [Authorize]
        // GET api/customers?FirstName=Neil&LastName=Effertz
        [HttpGet(Name = "GetCustomerBySearchCriteria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public IActionResult Get([FromQuery] CustomerSearchCriteria searchCriteria)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

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

        // POST api/customers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customerService.Add(customer);

            // return Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

          //  return CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);

            CustomerSearchCriteria customerSearchCriteria = new CustomerSearchCriteria { FirstName = customer.FirstName, LastName = customer.LastName, Gender = Gender.Male };
            return CreatedAtRoute("GetCustomerBySearchCriteria", customerSearchCriteria, customer);
        }

        // PUT api/customers/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            customerService.Update(customer);

            return NoContent();

        }

        // PATCH + JSONPATCH
        // http://jsonpatch.com/

        // Content-Type: application/merge-patch+json
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument jsonPatch)
        {
            Customer customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            jsonPatch.ApplyTo(customer);

            return NoContent();
        }

        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int id)
        {
            if (!customerService.Exists(id))
                return NotFound();

            customerService.Remove(id);

            return NoContent();
        }

        // HEAD api/customers/{id}
        [HttpHead("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Head(int id)
        {
            if (!customerService.Exists(id))
                return NotFound();

            return Ok();
        }

        // POST api/customers/{action}
        [HttpPost(nameof(Hello))]
        public IActionResult Hello([FromBody] string message)
        {
            return new JsonResult(message);
        }

    }
}
