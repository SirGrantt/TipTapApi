using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.EarningsDtos
{
    public class EarningDto
    {
        public DateTime ShiftDate { get; set; }
        public int Id { get; set; }
        public string JobWorked { get; set; }
        public string LunchOrDinner { get; set; }
        public int StaffMemberId { get; set; }

        //These two values will most often be false and zero, but are needed for unsual circumstances
        public bool OwedCashForTipOut { get; set; }
        public decimal CashPayedForTipOut { get; set; }

        public decimal CcTips { get; set; }
        public decimal AutoGratuity { get; set; }
        public decimal CashTips { get; set; }
        public decimal TotalTipsForPayroll { get; set; }
    }
}
