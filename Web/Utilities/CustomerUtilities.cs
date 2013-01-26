using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Utilities
{
    public static class CustomerUtilities
    {
        public static string GetFirstName(string fullName)
        {
            string firstName = string.Empty;
            string [] nameParts;
            if(fullName.Contains(","))
            {
                // Last name first - Smith, Sam
                nameParts = fullName.Split(',');
                if ((null != nameParts) && nameParts.Length > 1)
                {
                    firstName = nameParts[1].Trim();
                }
            }
            else
            {
                // First name first - Sam Smith
                nameParts = fullName.Split(' ');
                if ((null != nameParts) && nameParts.Length > 0)
                {
                    firstName = nameParts[0].Trim();
                }
            }

            return firstName;
        }

        public static string GetLastName(string fullName)
        {
            string lastName = string.Empty;
            string[] nameParts;
            if (fullName.Contains(","))
            {
                // Last name first - Smith, Sam
                nameParts = fullName.Split(',');
                if ((null != nameParts) && nameParts.Length > 0)
                {
                    lastName = nameParts[0].Trim();
                }
            }
            else
            {
                // First name first - Sam Smith
                nameParts = fullName.Split(' ');
                if ((null != nameParts) && nameParts.Length > 1)
                {
                    lastName = nameParts[1].Trim();
                }
            }

            return lastName;
        }

    }
}