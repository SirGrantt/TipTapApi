using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckOutDtos
{
    public class GetCheckoutsForShiftDto
    {
        public string StringDate { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }
    }
}
