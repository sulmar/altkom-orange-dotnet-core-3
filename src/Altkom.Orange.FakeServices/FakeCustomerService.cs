using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Altkom.Orange.Models.SearchCriterias;
using Bogus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Orange.FakeServices
{
    public class FakeCustomerServiceOptions
    {
        public int Quantity { get; set; }
    }

    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

        //public FakeCustomerService(Faker<Customer> faker, IOptions<FakeCustomerServiceOptions> options)
        //{
        //    customers = faker.Generate(options.Value.Quantity);
        //}

        public FakeCustomerService(Faker<Customer> faker, ILogger<FakeCustomerService> logger, FakeCustomerServiceOptions options = null)
        {
            logger.LogInformation("Generating customers");

            if (options==null)
            {
                options = new FakeCustomerServiceOptions { Quantity = 100 };
            }

            customers = faker.Generate(options.Quantity);
        }

        public void Add(Customer entity)
        {
            int lastId = customers.Max(c => c.Id);

            entity.Id = ++lastId;
            
            customers.Add(entity);
        }

        public bool Exists(int id)
        {
            return customers.Any(c => c.Id == id);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public Customer Get(string pesel)
        {
            return customers.SingleOrDefault(c => c.Pesel == pesel);
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            IQueryable<Customer> query = customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.FirstName))
            {
                query = query.Where(c => c.FirstName.Contains(searchCriteria.FirstName));
            }

            if (!string.IsNullOrEmpty(searchCriteria.LastName))
            {
                query = query.Where(c => c.FirstName.Contains(searchCriteria.LastName));
            }

            if (searchCriteria.From.HasValue)
            {
                query = query.Where(c => c.Birthday >= searchCriteria.From);
            }

            if (searchCriteria.To.HasValue)
            {
                query = query.Where(c => c.Birthday <= searchCriteria.To);
            }

            if (searchCriteria.Gender.HasValue)
            {
                query = query.Where(c => c.Gender == searchCriteria.Gender);
            }

            return query.ToList();
        }

        public Customer GetByUserName(string username)
        {
            return customers.SingleOrDefault(c => c.UserName == username);
        }

        public void Remove(int id)
        {
            Customer customer = Get(id);

            if (customer!=null)
                customer.IsRemoved = true;
        }

        public void Update(Customer entity)
        {
            Customer customer = Get(entity.Id);

            customer.FirstName = entity.FirstName;
            customer.LastName = entity.LastName;
            customer.Email = entity.Email;
            customer.Gender = entity.Gender;
            customer.Birthday = entity.Birthday;
            customer.CreditAmount = entity.CreditAmount;
        }
    }
}
