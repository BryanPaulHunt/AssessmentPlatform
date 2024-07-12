using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Services.Interfaces
{
    //Write Service Interface
    // -Split out to implement the Command and Query Responsibility Segregation (CQRS) pattern
    public interface ICustomerServiceWrite
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}