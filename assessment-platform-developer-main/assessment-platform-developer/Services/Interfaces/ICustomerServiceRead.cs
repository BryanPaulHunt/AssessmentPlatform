using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Services.Interfaces
{
    //Read Service Interface
    // -Split out to implement the Command and Query Responsibility Segregation (CQRS) pattern
    public interface ICustomerServiceRead
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
    }
}