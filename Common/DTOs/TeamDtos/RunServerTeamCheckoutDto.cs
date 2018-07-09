using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.TeamDtos
{
    public class RunServerTeamCheckoutDto
    {
        public int ServerTeamId { get; set; }
        public string StringDate { get; set; }
        public DateTime FormattedDate { get; set; }
        public string LunchOrDinner { get; set; }
    }
}
