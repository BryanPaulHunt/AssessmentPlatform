using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Services.Interfaces
{
    public interface ICustomerServiceRead
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
    }
}