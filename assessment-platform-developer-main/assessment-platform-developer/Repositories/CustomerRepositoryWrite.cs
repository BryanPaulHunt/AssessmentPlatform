using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Repositories
{
    //Write Repository Class
    // -Split out to implement the Command and Query Responsibility Segregation (CQRS) pattern
    public class CustomerRepositoryWrite: ICustomerRepositoryWrite
    {

        public void Add(Customer customer)
        {
            Database.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var existingCustomer = Database.Customers.FirstOrDefault(c => c.ID == customer.ID);
            if (existingCustomer != null)
            {
                // Update properties of existingCustomer based on the properties of customer
                // For example:
                existingCustomer.Name = customer.Name;
                // Repeat for other properties
            }
        }

        public void Delete(int id)
        {
            var customer = Database.Customers.FirstOrDefault(c => c.ID == id);
            if (customer != null)
            {
                Database.Customers.Remove(customer);
            }
        }
    }
}