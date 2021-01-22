using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Altkom.Orange.Models.SearchCriterias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Orange.EFDbServices
{
    public class DbCustomerService : ICustomerService
    {
        private readonly OrangeContext context;

        protected IEnumerable<Customer> entities => context.Customers;

        public DbCustomerService(OrangeContext context)
        {
            this.context = context;
        }

        public void Add(Customer entity)
        {
            context.Customers.Add(entity);
            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return context.Customers.Any(c => c.Id == id);
        }

        public Customer Get(string pesel)
        {
            return context.Customers.SingleOrDefault(c => c.Pesel == pesel);
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            IQueryable<Customer> query = context.Customers.AsQueryable();

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

        public IEnumerable<Customer> Get()
        {
            return context.Customers.AsNoTracking().ToList();

            var q = context.Customers.GroupBy(c => c.Gender)
                    .Select(g => new { Gender = g.Key, Quantity = g.Count() })
                    .AsQueryable();

            var results = q.ToList();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);
        }

        public Customer GetByUserName(string username)
        {
            // Microsoft.EntityFrameworkCore.SqlServer

            return context.Customers.FromSqlInterpolated($"select * from dbo.Customers where username = {username}").SingleOrDefault();

            // return context.Customers.SingleOrDefault(c => c.UserName == username);
        }

        public void Remove(int id)
        {
            //  Customer customer = Get(id);

            Customer customer = new Customer { Id = id };
            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        public void Update(Customer entity)
        {
            context.Customers.Update(entity);
            context.SaveChanges();
        }
    }
}
