using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.TeamDtos
{
    public class CreateServerTeamDto
    {
        public string UnformattedDate { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }
    }
}
