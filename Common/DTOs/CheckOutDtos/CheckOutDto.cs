﻿using Common.DTOs.JobDtos;
using Common.DTOs.StaffMemberDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckOutDtos
{
    public class CheckoutDto
    {
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public StaffMemberDto StaffMember { get; set; }
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
        public JobDto Job { get; set; }
        public decimal BarSpecialLine { get; set; }
        public decimal SaSpecialLine { get; set; }
    }
}
