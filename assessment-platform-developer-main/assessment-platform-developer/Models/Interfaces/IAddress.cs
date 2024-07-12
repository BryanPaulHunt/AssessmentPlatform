using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Models.Interfaces
{
    // I created an interface for Address, even though only one object (CustomerAddress)
    // implements it. My thoughts are that it is genericenough that other places may use 
    // this and that having an interface may be useful for unit testing
    public interface IAddress
    {
        string Address { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
        string Country { get; set; }
    }
}