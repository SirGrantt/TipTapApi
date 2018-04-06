using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public static class UtilityMethods
    {
        public static bool ValidateLunchOrDinnerSpecification(string data)
        {
            if (data.ToLower().Trim() == "dinner" || data.ToLower().Trim() == "lunch")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
