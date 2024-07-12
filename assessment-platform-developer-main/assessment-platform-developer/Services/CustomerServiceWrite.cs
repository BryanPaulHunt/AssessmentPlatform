using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Repositories.Interfaces;
using assessment_platform_developer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Services
{
    //Write Service Class
    // -Split out to implement the Command and Query Responsibility Segregation (CQRS) pattern
    public class CustomerServiceWrite: ICustomerServiceWrite
    {
        private readonly ICustomerRepositoryWrite customerRepository;

        public CustomerServiceWrite(ICustomerRepositoryWrite customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void AddCustomer(Customer customer)
        {
            customerRepository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerRepository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            customerRepository.Delete(id);
        }
    }
}