using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Repositories
{
    public class CustomerRepositoryRead: ICustomerRepositoryRead
    {
        public IEnumerable<Customer> GetAll()
        {
            return Database.Customers;
        }

        public Customer Get(int id)
        {
            return Database.Customers.FirstOrDefault(c => c.ID == id);
        }
    }
}