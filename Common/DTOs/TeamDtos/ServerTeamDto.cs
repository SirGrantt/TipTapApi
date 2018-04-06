using Common.DTOs.TipOutDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.TeamDtos
{
    public class ServerTeamDto
    {
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }
        public bool CheckoutHasBeenRun { get; set; }
        public TipOutDto TipOut { get; set; }
    }
}
