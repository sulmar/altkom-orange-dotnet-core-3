using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Altkom.Orange.Models.SearchCriterias;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Altkom.Orange.DapperDbServices
{

    // dotnet add package Dapper
    public class CustomerService : ICustomerService
    {
        private readonly IDbConnection connection;

        public CustomerService(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Add(Customer entity)
        {
            string sql = "insert ....";

            // connection.Execute(sql, new )

        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Get(string pesel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            string sql = "select * from dbo.Customers";

            return connection.Query<Customer>(sql).AsList();
        }

        public Customer Get(int id)
        {
            string sql = "select * from dbo.Customers where Id = @id";

            return connection.QuerySingleOrDefault<Customer>(sql, new { @id = id });

        }

        public Customer GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
