using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Models.Interfaces
{
    //I noticed this common grouping in both the customer and it's contact
    //So I abstracted it out into an interface and created a base class
    //Customer and CustomerContact both derive from this
    public interface IIdentifyable
    {
        string Name { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Notes { get; set; }
    }
}