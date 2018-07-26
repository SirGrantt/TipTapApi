using Domain.Checkouts;
using Domain.StaffEarnings;
using Domain.TipOuts;
using Domain.Utilities.EarningsCalculator;
using Domain.Utilities.TipOutCalculator;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Utilities.TipOutCalculator.JobTypeEnum;

namespace Domain.Teams
{
    public class BarTeam
    {
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }
        public List<Checkout> Checkouts { get; set; }
        public bool CheckoutHasBeenRun { get; set; }
        public TipOut TipOut { get; set; }
        public TipOutCalculator TipOutCalculator { get; set; }
        public BarEarningsCalculator EarningsCalculator { get; set; }

        public BarTeam(DateTime shiftDate)
        {
            ShiftDate = shiftDate;
            Checkouts = new List<Checkout>();
            TipOutCalculatorFactory factory = new TipOutCalculatorFactory();
            TipOutCalculator = new TipOutCalculator();
            TipOut = new TipOut(shiftDate);
            EarningsCalculator = new BarEarningsCalculator();

        }

        public List<Earnings> RunBarCheckout(decimal serverTips, int barbackCount)
        {
            decimal combinedBarAndServer = serverTips;
            decimal barCash = 0;
            foreach (Checkout c in Checkouts)
            {
                combinedBarAndServer += c.CcAutoGrat + c.CcTips + c.CashAutoGrat;
                barCash += c.CashTips;
            }
            if (barbackCount == 1)
            {
                TipOut.BarBackTipOut = TipOutCalculator.CalculateTipOut(combinedBarAndServer, .1m, 0);
            }
            else
            {
                TipOut.BarBackTipOut = TipOutCalculator.CalculateTipOut(combinedBarAndServer, .15m, 0);
            }

            TipOut.BarBackCashTipOut = TipOutCalculator.CalculateTipOut(barCash, .1m, 0);

            return EarningsCalculator.CalculateEarnings(this.Checkouts, this.TipOut, serverTips, this.ShiftDate, this.LunchOrDinner);
        }
    }
}
