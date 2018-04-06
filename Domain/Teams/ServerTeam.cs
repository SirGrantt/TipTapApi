using Domain.Jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Utilities;
using Domain.Checkouts;
using Domain.TipOuts;
using Domain.StaffEarnings;
using Domain.Utilities.TipOutCalculator;
using static Domain.Utilities.TipOutCalculator.JobTypeEnum;

namespace Domain.Teams
{
    public class ServerTeam
    {
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }
        public bool CheckoutHasBeenRun { get; set; }
        public List<Checkout> CheckOuts { get; set; }
        public TipOut TipOut { get; set; }
        public ITipOutCalculator TipOutCalculator { get; set; }
        public int BottleCount { get; set; }

        public ServerTeam(DateTime shiftDate)
        {
            ShiftDate = shiftDate;
            CheckOuts = new List<Checkout>();
            CheckoutHasBeenRun = false;
            TipOutCalculatorFactory factory = new TipOutCalculatorFactory();
            TipOutCalculator = factory.CreateCalculator(JobType.Server);
            TipOut = new TipOut(shiftDate);
        }

        public void ResetTipOuts()
        {
            TipOut.BarTipOut = 0;
            TipOut.SaTipOut = 0;
            CheckoutHasBeenRun = false;
        }

        public Earnings RunCheckout(decimal barSpecialLine, decimal saSpecialLine)
        {
            if (CheckoutHasBeenRun == true)
            {
                ResetTipOuts();
            }

            foreach (Checkout c in CheckOuts)
            {
                BottleCount += c.NumberOfBottlesSold;
            }
            TipOut.FinalTeamBarSales = TipOutCalculator.CalculateTeamBarSales(CheckOuts);
            TipOut.TeamGrossSales = TipOutCalculator.CalculateTeamGrossSales(CheckOuts);
            TipOut.BarTipOut = TipOutCalculator.CalculateTipOut(TipOut.FinalTeamBarSales, .05m, barSpecialLine) + BottleCount;
            TipOut.SaTipOut = TipOutCalculator.CalculateTipOut(TipOut.TeamGrossSales, .015m, saSpecialLine);
            Earnings earnings = TipOutCalculator.CalculateEarnings(CheckOuts, TipOut, ShiftDate, LunchOrDinner);

            CheckoutHasBeenRun = true;
            return earnings;
        }
    }
}
