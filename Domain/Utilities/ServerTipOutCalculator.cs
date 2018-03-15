using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Utilities
{
    public class ServerTipOutCalculator : ITipOutCalculator
    {
        public decimal CalculateTipOut(decimal target, decimal tipOutPercent)
        {
             if (target == 0)
            {
                throw new ArgumentException("The value provided to caluclate a tipOut on cannot be 0");
            }
            
             if (tipOutPercent == 0)
            {
                throw new ArgumentException("The value provided for the percent of a tip out cannot be 0");
            }

            decimal tipout = target * tipOutPercent;
            return tipout;
        }
    }
}
