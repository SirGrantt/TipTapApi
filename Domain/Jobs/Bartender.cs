using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Jobs
{
    public class Bartender
    {
        public int Id { get; set; }
        public StaffMember Employee { get; set; }
        public decimal GrossSales { get; set; }
        public decimal Sales { get; set; }
        public decimal BarSales { get; set; }
        public decimal CcTips { get; set; }
        public decimal CashTips { get; set; }
        public decimal CcAutoGratuity { get; set; }
        public decimal CashAutoGratuity { get; set; }
        public decimal Hours { get; set; }
        public decimal TipoutReceived { get; set; }
    }
}
