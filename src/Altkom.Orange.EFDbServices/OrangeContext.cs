using Altkom.Orange.EFDbServices.Configurations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<Customer>().HasKey(p => p.Id).HasName("_CustomerId");

            // modelBuilder.Entity<Customer>().ToTable("CRM.Klienci");

            // modelBuilder.Entity<Customer>().HasIndex(p=>p.Pesel).ForSqlServerIsClustered();

            //modelBuilder.Entity<Customer>().Property(p => p.FirstName)
            //    .HasMaxLength(50)
            //    .IsRequired();

            //modelBuilder.Entity<Customer>().Property(p => p.LastName)
            //   .HasMaxLength(50)
            //   .IsRequired();

            //modelBuilder.Entity<Customer>().Property(p => p.Pesel)
            //   .HasMaxLength(11)
            //   .IsUnicode(false)
            //   .IsRequired();

           // modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);

            modelBuilder.Entity<Order>().Property(p => p.OrderDate).HasColumnType("datetime");

        }
    }
}
