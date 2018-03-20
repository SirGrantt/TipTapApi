using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Groups;

namespace Domain.ShiftManager
{
    public class Shift
    {
        public DateTime ShiftDate { get; set; }
        public string ShiftDateFormatted { get; set; }
        public string LunchOrDinner { get; set; }
        public int Id { get; set; }
        public decimal ServerTipOutForBartenders { get; set; }
        public decimal ServerTipOutForSAs { get; set; }
        public ServerGroup ServerGroup { get; set; }

        public Shift(DateTime date, string lunchOrDinner)
        {
            ShiftDate = date;
            ShiftDateFormatted = ShiftDate.ToString("MM/dd/yyyy");
            LunchOrDinner = lunchOrDinner;
            ServerGroup = new ServerGroup();
        }
    }
}
