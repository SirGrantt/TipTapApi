using Application.PageFormatting;
using Common.DTOs.CheckOutDtos;
using Common.DTOs.EarningsDtos;
using Domain.Teams;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private GroupedCheckoutsFormatter groupFormatter;
        
        public Tests()
        {
            groupFormatter = new GroupedCheckoutsFormatter();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            DateTime shiftDate = DateTime.Now;
            CheckoutOverviewDto checkout1 = new CheckoutOverviewDto("Grant Elmer", "Bartender")
            {
                Sales = 1200,
                ShiftDate = shiftDate,
                BarSales = 300,
                CashTips = 0,
                CcTips = 230,
                StaffMemberId = 1,
                Hours = 6.5m,
                Id = 2,
                LunchOrDinner = "dinner",
            };
            CheckoutOverviewDto checkout2 = new CheckoutOverviewDto("Alyson Elmer", "Server")
            {
                Sales = 1200,
                ShiftDate = shiftDate,
                BarSales = 300,
                CashTips = 0,
                CcTips = 230,
                StaffMemberId = 1,
                Hours = 6.5m,
                Id = 2,
                LunchOrDinner = "dinner",
            };
            CheckoutOverviewDto checkout3 = new CheckoutOverviewDto("James Brooks", "Bartender")
            {
                Sales = 1200,
                ShiftDate = shiftDate,
                BarSales = 300,
                CashTips = 0,
                CcTips = 230,
                StaffMemberId = 1,
                Hours = 6.5m,
                Id = 3,
                LunchOrDinner = "dinner",
            };
            List<CheckoutOverviewDto> checkouts = new List<CheckoutOverviewDto>()
            {
                checkout1,
                checkout2,
                checkout3
            };
            EarningDto earning1 = new EarningDto
            {
                CcTips = 100,
                JobWorked = "Bartender",
                Id = 3,
                StaffMemberId = 1,
                LunchOrDinner = "Dinner",
                ShiftDate = shiftDate,
                CashTips = 0,
                TotalTipsForPayroll = 100,
            };
            EarningDto earning2 = new EarningDto
            {
                CcTips = 100,
                JobWorked = "Server",
                Id = 6,
                StaffMemberId = 2,
                LunchOrDinner = "Dinner",
                ShiftDate = shiftDate,
                CashTips = 0,
                TotalTipsForPayroll = 100,
            };
            EarningDto earning3 = new EarningDto
            {
                CcTips = 100,
                JobWorked = "Bartender",
                Id = 9,
                StaffMemberId = 3,
                LunchOrDinner = "Dinner",
                ShiftDate = shiftDate,
                CashTips = 0,
                TotalTipsForPayroll = 100,
            };
            List<EarningDto> earnings = new List<EarningDto>()
            {
                earning1,
                earning2,
                earning3
            };
            BarTeam barTeam = new BarTeam(shiftDate);
            barTeam.CheckoutHasBeenRun = true;

            TeamGroupedCheckoutsDto barData = groupFormatter.FormatBarCheckouts(barTeam, checkouts, earnings);

            Assert.Contains(earning1, barData.TeamEarnings);
            Assert.Contains(checkout3, barData.TeamCheckouts);
        }
    }
}