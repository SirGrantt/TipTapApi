using Domain.Jobs;
using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CheckOuts
{
    public class CheckOut
    {
        public DateTime ShiftDate { get; set; }
        public StaffMember StaffMember { get; set; }
        public decimal NonTipOutBarSales { get; set; }
        public int NumberOfBottlesSold { get; set; }
        public string JobWorked { get; set; }
        public string LunchOrDinner { get; set; }
        public decimal Sales { get; set; }
        public decimal GrossSales { get; set; }
        public decimal BarSales { get; set; }
        public decimal CcTips { get; set; }
        public decimal CashTips { get; set; }
        public decimal CcAutoGrat { get; set; }
        public decimal CashAutoGrat { get; set; }
        public decimal Hours { get; set; }

        public CheckOut(StaffMember staffMember, DateTime shiftDate, string jobWorked)
        {
            ShiftDate = shiftDate;
            StaffMember = staffMember;
            JobWorked = jobWorked;
        }
    }
}
