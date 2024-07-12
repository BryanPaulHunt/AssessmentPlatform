using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Repositories.Interfaces
{
    //Interface for Read repository
    // -Split out to implement the Command and Query Responsibility Segregation (CQRS) pattern
    public interface ICustomerRepositoryRead
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        int GetNextID();
    }
}