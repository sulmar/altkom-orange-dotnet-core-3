using Altkom.Orange.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.Orange.EFDbServices
{
    // dotnet add package Microsoft.EntityFrameworkCore
    public class OrangeContext : DbContext
    {
        public OrangeContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
