﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckOutDtos
{
    public class UpdateCheckoutDto
    {
        public int Id { get; set; }
        public string StringDate { get; set; }
        public decimal NonTipOutBarSales { get; set; }
        public int NumberOfBottlesSold { get; set; }
        public string LunchOrDinner { get; set; }
        public decimal Sales { get; set; }
        public decimal GrossSales { get; set; }
        public decimal BarSales { get; set; }
        public decimal CcTips { get; set; }
        public decimal CashTips { get; set; }
        public decimal CcAutoGrat { get; set; }
        public decimal CashAutoGrat { get; set; }
        public decimal Hours { get; set; }
        public int StaffMemberId { get; set; }
        public decimal BarSpecialLine { get; set; }
        public decimal SaSpecialLine { get; set; }
    }
}
