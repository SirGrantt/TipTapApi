using System;
using System.Collections.Generic;
using System.Text;
using Domain.Checkouts;
using Domain.StaffEarnings;
using Domain.TipOuts;
using static Domain.Utilities.TipOutCalculator.JobTypeEnum;

namespace Domain.Utilities.TipOutCalculator
{
    public class ServerTipOutCalculator : ITipOutCalculator
    {
        public JobType Job => JobType.Server;

        public Earnings CalculateEarnings(List<Checkout> checkouts, TipOut tipout, DateTime shiftDate, string lunchOrDinner)
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

            return individualEarnings;
        }

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
                throw new ArgumentException("The value provided to caluclate a tipOut on cannot be 0");
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
