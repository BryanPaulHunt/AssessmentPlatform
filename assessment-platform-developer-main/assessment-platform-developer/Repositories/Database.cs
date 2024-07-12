using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Repositories
{
    public static class Database
    {
        private static List<Customer> customers = new List<Customer>();

        public static List<Customer> Customers {
            get { return customers; }
            set { customers = value; }
    }
    }
}