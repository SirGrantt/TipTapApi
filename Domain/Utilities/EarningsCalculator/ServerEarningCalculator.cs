using System;
using System.Collections.Generic;
using System.Text;
using Domain.Checkouts;
using Domain.StaffEarnings;
using Domain.TipOuts;

namespace Domain.Utilities.EarningsCalculator
{
    public class ServerEarningCalculator
    {
        public List<Earnings> CalculateEarnings(List<Checkout> checkouts, TipOut tipout, DateTime shiftDate, string lunchOrDinner)
        {
            decimal teamTotalTipout = tipout.BarTipOut + tipout.SaTipOut;
            decimal teamTotalCcTips = 0;
            decimal teamTotalAutoGrat = 0;
            decimal teamTotalCashTips = 0;
            foreach (Checkout c in checkouts)
            {
                teamTotalCcTips += c.CcTips + c.CashAutoGrat;
                teamTotalAutoGrat += c.CcAutoGrat;
                teamTotalCashTips += c.CashTips;
            }

            Earnings individualEarnings = new Earnings(shiftDate, "server", lunchOrDinner);

            if (teamTotalCcTips >= teamTotalTipout)
            {
                decimal TeamCcTipsAfterCheckout = teamTotalCcTips - teamTotalTipout;
                individualEarnings.CcTips = Math.Round(TeamCcTipsAfterCheckout / checkouts.Count, 2);
                individualEarnings.AutoGratuity = Math.Round(teamTotalAutoGrat / checkouts.Count, 2);
                individualEarnings.CashTips = Math.Round(teamTotalCashTips / checkouts.Count, 2);
                individualEarnings.TotalTipsForPayroll = Math.Round(individualEarnings.CcTips + individualEarnings.CashTips + individualEarnings.AutoGratuity, 2);
            }
            else if (teamTotalCcTips + teamTotalAutoGrat > teamTotalTipout)
            {
                individualEarnings.CcTips = 0;
                decimal combinedAutoGratAndCC = teamTotalCcTips + teamTotalAutoGrat;
                individualEarnings.AutoGratuity = Math.Round(combinedAutoGratAndCC - teamTotalTipout, 2);
                individualEarnings.CashTips = Math.Round(teamTotalCashTips / checkouts.Count, 2);
                individualEarnings.TotalTipsForPayroll = Math.Round(individualEarnings.CashTips + individualEarnings.AutoGratuity, 2);
            }
            else
            {
                decimal teamTotalCCandAutoG = teamTotalCcTips + teamTotalAutoGrat;
                decimal remainingTipOutOwed = teamTotalTipout - teamTotalCCandAutoG;
                decimal remaingTeamCashTips = teamTotalCashTips - remainingTipOutOwed;
                individualEarnings.CcTips = 0;
                individualEarnings.AutoGratuity = 0;
                individualEarnings.OwedCashForTipOut = true;
                individualEarnings.CashPayedForTipOut = remainingTipOutOwed;
                individualEarnings.CashTips = Math.Round(remaingTeamCashTips / checkouts.Count, 2);
                individualEarnings.TotalTipsForPayroll = (individualEarnings.CashTips);
            }

            List<Earnings> teamEarnings = new List<Earnings>();
            foreach(Checkout c in checkouts)
            {
                teamEarnings.Add(individualEarnings);
            }
            return teamEarnings;
        }
    }
}
