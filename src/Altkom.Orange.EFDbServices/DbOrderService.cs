using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altkom.Orange.EFDbServices
{
    public class DbOrderService : IOrderService
    {
        private readonly OrangeContext context;

        public DbOrderService(OrangeContext context)
        {
            this.context = context;
        }

        public void Add(Order entity)
        {
            //Customer customer = new Customer() { Id = 100 };
            //context.Attach(customer);

            context.Orders.Add(entity);

            context.Entry(entity.Customer).State = EntityState.Unchanged;

            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> Get()
        {
            var removedOrders = context.Orders.Where(c => c.IsRemoved);

            var q = context.Orders.Except(removedOrders);

            // var orders = context.Customers.FromSqlInterpolated($"select * from dbo.Orders left outer ").SingleOrDefault();

            return q.ToList(); // <- strzal do bazy

            return context.Orders.Include(c=>c.Customer).Include(c=>c.Details).ToList();
        }

        public Order Get(int id)
        {
            return context.Orders.Find(id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
