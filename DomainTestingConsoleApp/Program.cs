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
        public static void WriteTeamCalculations(Shift shift, ServerTeam team)
        {
            Console.WriteLine("BarTenders TipOut: " + shift.ServerGroup.ServersTipOutToBar.ToString());
            Console.WriteLine("SA TipOut: " + shift.ServerGroup.ServersTipOutToSAs.ToString());
            Console.WriteLine("Individual CC tip final: " + team.IndividualFinalCcTips.ToString());

            Console.WriteLine("Servers CC tips:");
            foreach (Server server in team.Servers)
            {
                Console.WriteLine("{0}: {1}", server.Employee.FirstName, server.CcTipTakehome);
            }

        }
        static void Main(string[] args)
        {
            ServerTeamEditor serverTeamEditor = new ServerTeamEditor();
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

            StaffMember Corderito = new StaffMember()
            {
                FirstName = "Corderito",
                LastName = "Elmer",
                Id = 3
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

            Server serverNathan = new Server()
            {
                Employee = Corderito,
                Sales = 100,
                GrossSales = 1202.56m,
                BarSales = 153,
                NonTipoutBarSales = 46,
                Hours = 6.5m,
                Id = 3,
                CcTips = 175,
                CcAutoGratuity = 40,
                CashAutoGratuity = 15,
                CashTips = 0
            };

            ServerTeam ourTeam = new ServerTeam() { Id = 1, NumberOfBottlesSold = 1};
            serverTeamEditor.AddTeamMember(ourTeam, serverAlyson);
            serverTeamEditor.AddTeamMember(ourTeam, serverGrant);
            //ourTeam.AddServer(serverAlyson);
            //ourTeam.AddServer(serverGrant);
            shift.ServerGroup.AddServerTeam(ourTeam);
            shift.ServerGroup.RunServerCheckOut(1);

            WriteTeamCalculations(shift, ourTeam);

            shift.ServerGroup.RunServerCheckOut(1);

            Console.WriteLine(" ");

            serverTeamEditor.RemoveTeamMember(shift.ServerGroup, ourTeam.Id, Alyson.Id);

            Console.WriteLine(" ");

            WriteTeamCalculations(shift, ourTeam);

            serverTeamEditor.AddTeamMember(ourTeam, serverNathan);
            shift.ServerGroup.RunServerCheckOut(ourTeam.Id);

            Console.WriteLine(" ");

            WriteTeamCalculations(shift, ourTeam);

            Console.ReadLine();
        }
    }
}
