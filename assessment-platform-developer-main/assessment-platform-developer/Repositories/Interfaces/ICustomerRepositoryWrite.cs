using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Repositories.Interfaces
{
    //Interface for Write repository
    // -Split out to implement the Command and Query Responsibility Segregation (CQRS) pattern
    public interface ICustomerRepositoryWrite
    {
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}