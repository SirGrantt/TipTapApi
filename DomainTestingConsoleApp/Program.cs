using Domain.ShiftManager;
using Domain.Groups;
using Domain.Jobs;
using Domain.StaffMembers;
using Domain.Teams;
using Domain.Utilities;
using System;

namespace DomainTestingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Shift shift = new Shift();
            StaffMember Grant = new StaffMember()
            {
                FirstName = "Grant",
                LastName = "Elmer",
                Id = 1
            };

            StaffMember Alyson = new StaffMember()
            {
                FirstName = "Alyson",
                LastName = "Elmer",
                Id = 2
            };

            Server serverGrant = new Server()
            {
                Employee = Grant,
                Sales = 839.86m,
                GrossSales = 544.08m,
                BarSales = 59,
                NonTipoutBarSales = 0,
                Hours = 6.5m,
                Id = 1,
                CcTips = 98,
                CcAutoGratuity = 0,
                CashAutoGratuity = 0,
                CashTips = 15
            };

            Server serverAlyson = new Server()
            {
                Employee = Alyson,
                Sales = 900,
                GrossSales = 839.86m,
                BarSales = 146,
                NonTipoutBarSales = 0,
                Hours = 6.5m,
                Id = 2,
                CcTips = 234,
                CcAutoGratuity = 0,
                CashAutoGratuity = 0,
                CashTips = 0
            };

            ServerTeam ourTeam = new ServerTeam() { Id = 1};
            ourTeam.AddServer(serverAlyson);
            ourTeam.AddServer(serverGrant);
            shift.ServerGroup.AddServerTeam(ourTeam);
            shift.ServerGroup.RunServerCheckOut(1);

            Console.WriteLine("BarTenders TipOut: " + shift.ServerGroup.ServersTipOutToBar.ToString());
            Console.WriteLine("SA TipOut: " + shift.ServerGroup.ServersTipOutToSAs.ToString());
            Console.WriteLine("Individual CC tip final: " + ourTeam.IndividualFinalCcTips.ToString());

            Console.WriteLine("Servers CC tips:");
            foreach (Server server in ourTeam.Servers)
            {
                Console.WriteLine("{0}: {1}", server.Employee.FirstName, server.CcTipTakehome);
            }

            Console.ReadLine();
        }
    }
}
