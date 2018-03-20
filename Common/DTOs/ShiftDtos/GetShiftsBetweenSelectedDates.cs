using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.ShiftDtos
{
    public class GetShiftsBetweenSelectedDates
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
