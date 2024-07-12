using assessment_platform_developer.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Models
{
    //Customer address seemed like a unique grouping of fields, so I put them
    // intheir own class which implements IAddress
    [Serializable]
    public class CustomerAddress: IAddress
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }
}