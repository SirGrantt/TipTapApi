﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.TipOuts
{
    public class TipOut
    {
        public int Id { get; set; }
        public decimal TeamGrossSales { get; set; }
        public decimal FinalTeamBarSales { get; set; }
        public decimal SaTipOut { get; set; }
        public decimal BarTipOut { get; set; }
        public DateTime ShiftDate { get; set; }

        public TipOut(DateTime shiftDate)
        {
            ShiftDate = shiftDate;
        }

    }
}