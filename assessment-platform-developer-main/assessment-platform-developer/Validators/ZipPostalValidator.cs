using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace assessment_platform_developer.Validators
{
    public class ZipPostalValidator
    {
        private static string usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
        private static string caPostalRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

        public bool IsZipCode(string zipCode)
        {
            if (Regex.Match(zipCode, usZipRegEx).Success)
            {
                return true;
            }
            return false;
        }

        public bool IsPostalCode(string postalCode)
        {
            if (Regex.Match(postalCode, caPostalRegEx).Success)
            {
                return true;
            }
            return false;
        }

        public bool IsCorrectPostalForCountry(string postalCode, string country)
        {
            if (country.ToUpper() == "CANADA")
            {
                return IsPostalCode(postalCode);
            }
            if (country.ToUpper() == "UNITED STATES")
            {
                return IsZipCode(postalCode);
            }

            return false;
        }
    }
}