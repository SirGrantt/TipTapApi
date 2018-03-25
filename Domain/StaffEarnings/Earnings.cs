using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.StaffEarnings
{
    public class Earnings
    {
        public DateTime ShiftDate { get; set; }
        public int Id { get; set; }
        public string JobWorked { get; set; }
        public bool OwedCashForTipOut { get; set; }
        public decimal CashPayedForTipOut { get; set; }
        public decimal CcTips { get; set; }
        public decimal AutoGratuity { get; set; }
        public decimal TotalTipsForPayroll { get; set; }
        public decimal CashTips { get; set; }

        public Earnings(DateTime shiftDate, string jobWorked)
        {
            ShiftDate = shiftDate;
            OwedCashForTipOut = false;
            JobWorked = jobWorked;
        }
    }
}
