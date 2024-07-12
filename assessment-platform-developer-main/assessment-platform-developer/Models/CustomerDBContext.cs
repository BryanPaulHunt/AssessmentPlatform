using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Models
{
    //Even though this is SqlUserDefinedAggregateAttribute nowhere, I moved it to it's
    // own file for clarity
    public class CustomerDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}