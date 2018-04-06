using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.TipOutDtos
{
    public class TipOutDto
    {
        public int Id { get; set; }
        public decimal TeamGrossSales { get; set; }
        public decimal FinalTeamBarSales { get; set; }
        public decimal SaTipOut { get; set; }
        public decimal BarTipOut { get; set; }
        public DateTime ShiftDate { get; set; }
    }
}
