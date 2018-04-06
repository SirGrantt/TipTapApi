using Domain.Checkouts;
using Domain.Jobs;
using Domain.StaffEarnings;
using Domain.StaffMembers;
using Domain.Teams;
using Domain.Utilities;
using System;
using System.Globalization;

namespace DomainTestingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //each server prints a their  indiv checkout
            //once all servers that are on a team have printed  their checkouts they bring to manager
            //mgr is going to create the server tip out for each server on the team 
            //  a server tip out keeps track of date, server name, gross sales, etc

            Job server = new Job() { Title = "Server" };

            StaffMember Grant = new StaffMember()
            {
                FirstName = "Grant",
                LastName = "Elmer",
                Id = 1,
            };

            StaffMember Alyson = new StaffMember()
            {
                FirstName = "Alyson",
                LastName = "Elmer",
                Id = 2,
            };

            StaffMember Lauren = new StaffMember()
            {
                FirstName = "Lauren",
                LastName = "Wine",
                Id = 3,
            };

            ServerTeam team = new ServerTeam(DateTime.Today);

            Checkout grantsCheckOut = new Checkout(Grant, DateTime.Today, server)
            {
                GrossSales = 962.92m,
                Sales = 900,
                BarSales = 181,
                LunchOrDinner = "dinner",
                CashAutoGrat = 10,
                CcAutoGrat = 47.39m,
                CcTips = 83.64m,
                NonTipOutBarSales = 0,
                Hours = 6, 
                CashTips = 0,
                NumberOfBottlesSold = 1
            };

            Checkout alysonsCheckOut = new Checkout(Alyson, DateTime.Today, server)
            {
                GrossSales = 1680.78m,
                Sales = 800,
                BarSales = 308.50m,
                LunchOrDinner = "dinner",
                CashAutoGrat = 0,
                CcAutoGrat = 0,
                CcTips = 323,
                NonTipOutBarSales = 0,
                Hours = 6,
                CashTips = 0,
                NumberOfBottlesSold = 2 
            };

            Checkout laurensCheckOut = new Checkout(Alyson, DateTime.Today, server)
            {
                GrossSales = 2187.03m,
                Sales = 800,
                BarSales = 354,
                LunchOrDinner = "dinner",
                CashAutoGrat = 0,
                CcAutoGrat = 354.88m,
                CcTips = 1541,
                NonTipOutBarSales = 0,
                Hours = 6,
                CashTips = 0,
                NumberOfBottlesSold = 0
            };

            team.CheckOuts.Add(grantsCheckOut);
            team.CheckOuts.Add(alysonsCheckOut);
            team.CheckOuts.Add(laurensCheckOut);
            Earnings teamMemberEarnings;
            decimal barSpecialLine = grantsCheckOut.NumberOfBottlesSold + alysonsCheckOut.NumberOfBottlesSold;
            teamMemberEarnings = team.RunCheckout(barSpecialLine, 0);

            Console.WriteLine("CC Tips: " + teamMemberEarnings.CcTips.ToString());
            Console.WriteLine("AutoGrat: " + teamMemberEarnings.AutoGratuity.ToString());
            Console.WriteLine("Total Tips: " + teamMemberEarnings.TotalTipsForPayroll.ToString());

            Console.WriteLine("Teams TipOut Numbers: ");
            Console.WriteLine("Team Gross Sales: " + team.TipOut.TeamGrossSales.ToString());
            Console.WriteLine("Team Bottles Sold: " + barSpecialLine.ToString());
            Console.WriteLine("Bar: " + team.TipOut.BarTipOut.ToString());
            Console.WriteLine("SA: " + team.TipOut.SaTipOut.ToString());

            Console.ReadLine();

        }
    }
}
