using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Repositories.Interfaces
{
    public interface ICustomerRepositoryWrite
    {
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}