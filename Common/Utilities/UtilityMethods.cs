using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public static class UtilityMethods
    {
        public static void ValidateLunchOrDinnerSpecification(string data)
        {
            if (data.ToLower().Trim() == "dinner" || data.ToLower().Trim() == "lunch")
            {
                return;
            }
            else
            {
                throw new InvalidOperationException("Lunch or Dinner needs to be specified");
            }
        }

        public static void VerifyDatabaseSaveSuccess(this IRepository repository)
        {
            if (!repository.Save())
            {
                throw new Exception("An unexpected error occured while trying to save the changes to the Database.");
            }
        }
    }
}
