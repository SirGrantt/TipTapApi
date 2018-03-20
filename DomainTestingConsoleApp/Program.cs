using Domain.ShiftManager;
using Domain.Groups;
using Domain.Jobs;
using Domain.StaffMembers;
using Domain.Teams;
using Domain.Utilities;
using System;
using System.Globalization;

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
                Console.WriteLine("{0}: {1}", server.StaffMember.FirstName, server.CcTipTakehome);
            }

        }
        static void Main(string[] args)
        {
            ServerTeamEditor serverTeamEditor = new ServerTeamEditor();
            Shift shift = new Shift(DateTime.Now.Date, "dinner");
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
                StaffMember = Grant,
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
                StaffMember = Alyson,
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
                StaffMember = Corderito,
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

            shift.ServerGroup.AddServerTeam(ourTeam);
            shift.ServerGroup.RunServerCheckOut(1);


            WriteTeamCalculations(shift, ourTeam);

            shift.ServerGroup.RunServerCheckOut(1);

            WriteTeamCalculations(shift, ourTeam);

            Console.WriteLine(" ");

            serverTeamEditor.RemoveTeamMember(shift.ServerGroup, ourTeam.Id, Alyson.Id);

            Console.WriteLine(" ");

            WriteTeamCalculations(shift, ourTeam);

            serverTeamEditor.AddTeamMember(ourTeam, serverNathan);
            shift.ServerGroup.RunServerCheckOut(ourTeam.Id);

            Console.WriteLine(" ");

            WriteTeamCalculations(shift, ourTeam);

            Console.WriteLine("*AFTER TEAMMEMBER UPDATED*");
            serverGrant.CcTips = 198.65m;
            serverTeamEditor.UpdateTeamMember(shift.ServerGroup, ourTeam.Id, serverGrant);
            shift.ServerGroup.RunServerCheckOut(ourTeam.Id);

            WriteTeamCalculations(shift, ourTeam);

            var date = DateTime.Now.ToShortDateString();
            var ddate = DateTime.Today;

            Console.WriteLine(date);

            var date2 = DateTime.Today.Month;
            var date3 = DateTime.Today.Day;
            var date4 = DateTime.Today.Year;

            Console.WriteLine(date.ToString());
            Console.WriteLine(date2.ToString());
            Console.WriteLine(date3.ToString());
            Console.WriteLine(date4.ToString());
            string formattedDate = "0" + date2.ToString() + "-" + date3 + "-" + date4;
            Console.WriteLine(DateTime.Today.ToString());

            

            Console.WriteLine("Shift Date is: " + shift.ShiftDateFormatted);
            formattedDate = ddate.ToString("MM/dd/yyyy");
            DateTime converted = Convert.ToDateTime(formattedDate);
            shift.ShiftDate = converted;
            Console.WriteLine("Converted DateTime Added: " + shift.ShiftDate.ToString());
            Console.WriteLine(formattedDate);
            Console.ReadLine();
        }
    }
}
