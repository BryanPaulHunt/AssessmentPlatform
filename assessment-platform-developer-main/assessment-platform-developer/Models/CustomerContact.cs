using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Models
{
    //Customer Contact seemed like a unique grouping of fields (namely an Identity + a Title)
    //, so I put them in their own class which inherits from Identity (IIdentity)
    [Serializable]
    public class CustomerContact: Identity 
    {
        public string Title { get; set; }
    }
}