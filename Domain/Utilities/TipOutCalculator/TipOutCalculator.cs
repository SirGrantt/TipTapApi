using System;
using System.Collections.Generic;
using System.Text;
using Domain.Checkouts;
using Domain.StaffEarnings;
using Domain.TipOuts;
using static Domain.Utilities.TipOutCalculator.JobTypeEnum;

namespace Domain.Utilities.TipOutCalculator
{
    public class TipOutCalculator
    {

        public decimal CalculateTeamBarSales(List<Checkout> checkouts)
        {
            decimal teamBarSales = 0;

            foreach(Checkout c in checkouts)
            {
                teamBarSales += c.BarSales - c.NonTipOutBarSales;
            }

            return teamBarSales;
        }

        public decimal CalculateTeamGrossSales(List<Checkout> checkouts)
        {
            decimal teamGrossSales = 0;

            foreach (Checkout checkout in checkouts)
            {
                teamGrossSales += checkout.GrossSales;
            }

            return teamGrossSales;
        }

        public decimal CalculateTipOut(decimal target, decimal tipOutPercent, decimal specialLine)
        {
             if (target == 0)
            {
                return 0;
            }
            
             if (tipOutPercent == 0)
            {
                throw new ArgumentException("The value provided for the percent of a tip out cannot be 0");
            }

            decimal tipout = target * tipOutPercent;

            if (specialLine > 0)
            {
                tipout += specialLine;
            }
            return Math.Round(tipout, 2);
        }
    }
}
