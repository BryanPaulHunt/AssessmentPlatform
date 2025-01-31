﻿using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Repositories
{
    //Read Repository Class
    // -Split out to implement the Command and Query Responsibility Segregation (CQRS) pattern
    public class CustomerRepositoryRead: ICustomerRepositoryRead
    {
        public IEnumerable<Customer> GetAll()
        {
            return Database.Customers;
        }

        public Customer Get(int id)
        {
            return Database.Customers.FirstOrDefault(c => c.ID == id);
        }

        public int GetNextID()
        {
            int biggestID = 0;

            foreach (Customer customer in Database.Customers)
            {
                if (customer.ID > biggestID) { biggestID = customer.ID; }
            }

            return biggestID + 1;
        }
    }
}