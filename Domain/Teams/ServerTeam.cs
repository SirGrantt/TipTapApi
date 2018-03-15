using Domain.Jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Utilities;

namespace Domain.Teams
{
    public class ServerTeam
    {
        public int Id { get; set; }
        public bool CheckoutHasBeenRun { get; set; }
        public List<Server> Servers { get; set; }
        public ITipOutCalculator TipOutCalculator { get; set; }
        public decimal IndividualFinalCcTips { get; set; }
        public decimal TeamCcTipsAfterCheckout { get; set; }
        public decimal IndividualFinalAutoGratTakeHome { get; set; }
        public decimal IndividualFinalCCandAutoGratTakeHome { get; set; }
        public decimal IndividualFinalCashTakeHome { get; set; }
        public decimal BarTipOut { get; set; }
        public decimal SATipOut { get; set; }
        public int NumberOfBottlesSold { get; set; }

        public ServerTeam()
        {
            Servers = new List<Server>();
            TipOutCalculator = new ServerTipOutCalculator();
            CheckoutHasBeenRun = false;
        }

        public decimal CalculateBarTipOut()
        {
            decimal totalTipOut = 0;
            if (Servers.Count == 0)
            {
                return totalTipOut;
            }

            foreach (Server server in Servers)
            {
                decimal barSalesToTipOutOn = server.BarSales - server.NonTipoutBarSales;
                totalTipOut += TipOutCalculator.CalculateTipOut(barSalesToTipOutOn, .05m);
            }

            totalTipOut += NumberOfBottlesSold;
            return Math.Round(totalTipOut, 2);
        }

        public decimal CalculateSATipOut()
        {
            decimal totalTipOut = 0;
            if (Servers.Count == 0)
            {
                return totalTipOut;
            }

            foreach (Server server in Servers)
            {
                totalTipOut += TipOutCalculator.CalculateTipOut(server.GrossSales, .015m);
            }
            return Math.Round(totalTipOut, 2);
        }

        public void ResetTipOuts()
        {
            BarTipOut = 0;
            SATipOut = 0;
            CheckoutHasBeenRun = false;
        }

        public void RunCheckout()
        {
            if (CheckoutHasBeenRun == true)
            {
                ResetTipOuts();
            }

            BarTipOut = CalculateBarTipOut();
            SATipOut = CalculateSATipOut();

            decimal teamTotalTipout = BarTipOut + SATipOut;
            decimal teamTotalCcTips = 0;
            decimal teamTotalAutoGrat = 0;
            decimal teamTotalCashTips = 0;
            foreach (Server server in Servers)
            {
                teamTotalCcTips += server.CcTips + server.CashAutoGratuity;
                teamTotalAutoGrat += server.CcAutoGratuity;
                teamTotalCashTips += server.CashTips;
            }

            if (teamTotalTipout > teamTotalCcTips)
            {
                return;
            }

            TeamCcTipsAfterCheckout = teamTotalCcTips - teamTotalTipout;
            IndividualFinalCcTips = TeamCcTipsAfterCheckout / Servers.Count;
            IndividualFinalAutoGratTakeHome = teamTotalAutoGrat / Servers.Count;
            IndividualFinalCashTakeHome = teamTotalCashTips / Servers.Count;
            IndividualFinalCCandAutoGratTakeHome = IndividualFinalCcTips + IndividualFinalAutoGratTakeHome;

            foreach (Server server in Servers)
            {
                server.CcTipTakehome = Math.Round(IndividualFinalCcTips, 2);
                server.CashTipTakeHome = Math.Round(IndividualFinalCashTakeHome, 2);
                server.AutoGratTakeHome = Math.Round(IndividualFinalAutoGratTakeHome, 2);
                server.TotalTipTakehome = IndividualFinalAutoGratTakeHome + IndividualFinalCcTips;
            }

            CheckoutHasBeenRun = true;
        }
    }
}
