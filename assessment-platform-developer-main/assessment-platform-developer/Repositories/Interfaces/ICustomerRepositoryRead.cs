using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Repositories.Interfaces
{
    public interface ICustomerRepositoryRead
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
    }
}