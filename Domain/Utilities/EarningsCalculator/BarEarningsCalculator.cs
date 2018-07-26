using System;
using System.Collections.Generic;
using System.Text;
using Domain.Checkouts;
using Domain.StaffEarnings;
using Domain.TipOuts;

namespace Domain.Utilities.EarningsCalculator
{
    public class BarEarningsCalculator
    { //Dont forget to update TipOuts across the board with BarBackTipOut
        public List<Earnings> CalculateEarnings(List<Checkout> checkouts, TipOut tipout, decimal serverTips, DateTime shiftDate, string lunchOrDinner)
        {
            var teamEarnings = new List<Earnings>();

            decimal totalTips = serverTips;
            decimal totalAutoGratuity = 0;
            decimal totalCashTips = 0;
            decimal totalTeamHours = 0;

            // Add up all the total team numbers
            foreach (Checkout c in checkouts)
            {
                totalTips += c.CcAutoGrat + c.CcTips + c.CashAutoGrat;
                totalAutoGratuity += c.CcAutoGrat;
                totalCashTips += c.CashTips;
                totalTeamHours += c.Hours;
            }

            //Take out the tipout and split up auto grat and cc
            decimal totalNetTips = totalTips - tipout.BarBackTipOut;
            totalNetTips = totalNetTips - totalAutoGratuity;

            //Determine team hourly pay
            decimal teamCcHourly = totalNetTips / totalTeamHours;
            decimal teamAutoGHourly = totalAutoGratuity / totalTeamHours;
            decimal teamCashHourly = totalCashTips / totalTeamHours;

            foreach (Checkout c in checkouts)
            {
                var earning = new Earnings(shiftDate, "bartender", lunchOrDinner)
                {
                    CcTips = c.Hours * teamCcHourly,
                    CashTips = c.Hours * teamCashHourly,
                    AutoGratuity = c.Hours * teamAutoGHourly,
                    StaffMember = c.StaffMember
                };
                earning.TotalTipsForPayroll = earning.CcTips + earning.AutoGratuity + earning.CashTips;
                teamEarnings.Add(earning);
            }

            return teamEarnings;
        }
    }
}
