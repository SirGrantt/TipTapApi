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
using Domain.Utilities.EarningsCalculator;

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
        public TipOutCalculator TipOutCalculator { get; set; }
        public ServerEarningCalculator EarningCalculator { get; set; }
        public int BottleCount { get; set; }
        public decimal TeamBarSpecialLine { get; set; }
        public decimal TeamSaSpecialLine { get; set; }

        public ServerTeam(DateTime shiftDate)
        {
            ShiftDate = shiftDate;
            CheckOuts = new List<Checkout>();
            CheckoutHasBeenRun = false;
            TipOutCalculator = new TipOutCalculator();
            EarningCalculator = new ServerEarningCalculator();
            TipOut = new TipOut(shiftDate, LunchOrDinner, "server");
        }

        public void ResetTipOuts()
        {
            TipOut.BarTipOut = 0;
            TipOut.SaTipOut = 0;
            CheckoutHasBeenRun = false;
        }

        public List<Earnings> RunCheckout()
        {
            if (CheckoutHasBeenRun == true)
            {
                ResetTipOuts();
            }

            foreach (Checkout c in CheckOuts)
            {
                // first reset these values to be sure no old data is being processed
                TeamSaSpecialLine = 0;
                TeamBarSpecialLine = 0;

                BottleCount += c.NumberOfBottlesSold;
                TeamBarSpecialLine += c.BarSpecialLine;
                TeamSaSpecialLine += c.SaSpecialLine;
            }
            TipOut.FinalTeamBarSales = TipOutCalculator.CalculateTeamBarSales(CheckOuts);
            TipOut.TeamGrossSales = TipOutCalculator.CalculateTeamGrossSales(CheckOuts);
            TipOut.BarTipOut = TipOutCalculator.CalculateTipOut(TipOut.FinalTeamBarSales, .05m, TeamBarSpecialLine) + BottleCount;
            TipOut.SaTipOut = TipOutCalculator.CalculateTipOut(TipOut.TeamGrossSales, .015m, TeamSaSpecialLine);
            List<Earnings> earnings = EarningCalculator.CalculateEarnings(CheckOuts, TipOut, ShiftDate, LunchOrDinner);

            CheckoutHasBeenRun = true;
            return earnings;
        }
    }
}
