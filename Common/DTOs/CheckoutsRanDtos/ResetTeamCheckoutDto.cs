using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckoutsRanDtos
{
    public class ResetTeamCheckoutDto
    {
        public int ServerTeamId { get; set; }
        public string UnformattedDate { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }
    }
}
