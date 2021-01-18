﻿using Altkom.Orange.IServices;
using Altkom.Orange.Models;
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
            customers.Add(entity);
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

        public void Remove(int id)
        {
            Customer customer = Get(id);

            if (customer!=null)
                customer.IsRemoved = true;
        }

        public void Udpate(Customer entity)
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