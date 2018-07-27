using Common.DTOs.CheckOutDtos;
using Domain.Checkouts;
using Domain.Jobs;
using Domain.StaffEarnings;
using Domain.StaffMembers;
using Domain.Teams;
using Domain.Utilities.EarningsCalculator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTesting
{
    public class TestingBarEarningCalculator
    {
        private DateTime ShiftDate;
        private BarTeam Team;
        private StaffMember StaffMember1;
        private StaffMember StaffMember2;
        private Job Bartender;

        public TestingBarEarningCalculator()
        {

            ShiftDate = DateTime.Now;
            Bartender = new Job()
            {
                Id = 2,
                Title = "bartender"
            };
            Team = new BarTeam(ShiftDate)
            {
                CheckoutHasBeenRun = false
            };
            StaffMember1 = new StaffMember()
            {
                FirstName = "Grant",
                LastName = "Elmer",
                Id = 1,
                Status = "acitve"
            };

            StaffMember2 = new StaffMember()
            {
                FirstName = "James",
                LastName = "Brooks",
                Id = 2,
                Status = "active"
            };
        }

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void EarningsShouldBeGeneratedForEachTeamMember()
        {

            Checkout checkout1 = new Checkout(StaffMember1, ShiftDate, Bartender)
            {
                Sales = 1200,
                ShiftDate = ShiftDate,
                BarSales = 300,
                CashTips = 0,
                CcTips = 230,
                Hours = 7m,
                LunchOrDinner = "dinner",
            };
            Checkout checkout3 = new Checkout(StaffMember2, ShiftDate, Bartender)
            {
                Sales = 900,
                ShiftDate = ShiftDate,
                BarSales = 100,
                CashTips = 0,
                CcTips = 120,
                Hours = 5.5m,
                LunchOrDinner = "dinner",
            };

            Team.Checkouts.Add(checkout1);
            Team.Checkouts.Add(checkout3);
            List<Earnings> earnings = Team.RunBarCheckout(123, 1);
            var grantsEarning = earnings.FirstOrDefault(e => e.StaffMember.Id == 1);
            var jamesEarning = earnings.FirstOrDefault(e => e.StaffMember.Id == 2);

            Assert.IsTrue(earnings.Any(e => e.StaffMember.Id == 1));
            Assert.IsTrue(earnings.Any(e => e.StaffMember.Id == 2));
            Assert.AreNotEqual(earnings[0], earnings[1]);
            Assert.IsTrue(grantsEarning.CcTips > jamesEarning.CcTips);
            
        }
    }
}
