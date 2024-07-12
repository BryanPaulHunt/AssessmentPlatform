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
    //Read Service Class
    // -Split out to implement the Command and Query Responsibility Segregation (CQRS) pattern
    public class CustomerServiceRead:ICustomerServiceRead
    {
        private readonly ICustomerRepositoryRead customerRepository;

        public CustomerServiceRead(ICustomerRepositoryRead customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return customerRepository.GetAll();
        }

        public Customer GetCustomer(int id)
        {
            return customerRepository.Get(id);
        }
    }
}