using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace assessment_platform_developer.Models
{
    //Moved to it's own class to ensure it isn't hidden
    public enum Countries
    {
        Canada,
        [Description("United States")]
        UnitedStates
    }
}