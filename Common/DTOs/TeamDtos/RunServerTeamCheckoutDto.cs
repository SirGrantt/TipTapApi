using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.TeamDtos
{
    public class RunServerTeamCheckoutDto
    {
        public int ServerTeamId { get; set; }
        public decimal BarSpecialLine { get; set; }
        public decimal SaSpecialLine { get; set; }
    }
}
