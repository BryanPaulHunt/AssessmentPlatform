using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using assessment_platform_developer.Models.Interfaces;

namespace assessment_platform_developer.Models
{
    //I noticed this common grouping in both the customer and it's contact
    //So I abstracted it out into an interface and created a base class
    //Customer and CustomerContact both derive from this
    [Serializable]
    public class Identity : IIdentifyable
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
    }
}