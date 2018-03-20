using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class ShiftEntity
    {
        public DateTime ShiftDate { get; set; }
        public string ShiftDateFormatted { get; set; }
        public string LunchOrDinner { get; set; }
        public int Id { get; set; }
        public decimal ServerTipOutForBartenders { get; set; }
        public decimal ServerTipOutForSAs { get; set; }
        public ServerGroupEntity ServerGroup { get; set; }
    }
}
