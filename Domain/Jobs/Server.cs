using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Jobs
{
    public class Server
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
        public decimal NonTipoutBarSales { get; set; }
        public decimal Hours { get; set; }
        public decimal CcTipTakehome { get; set; }
        public decimal CashTipTakeHome { get; set; }
        public decimal AutoGratTakeHome { get; set; }
        public decimal TotalTipTakehome { get; set; }

    }
}
