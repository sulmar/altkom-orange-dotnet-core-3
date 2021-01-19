using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Altkom.Orange.Models.SearchCriterias;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Orange.FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerService(Faker<Customer> faker)
        {
            customers = faker.Generate(100);
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
