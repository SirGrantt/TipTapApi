using Common.DTOs.GroupDtos;
using Domain.Groups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.ShiftDtos
{
    public class ShiftDto
    {
        public DateTime ShiftDate { get; set; }
        public string ShiftDateFormatted { get; set; }
        public string LunchOrDinner { get; set; }
        public int Id { get; set; }
        public decimal ServerTipOutForBartenders { get; set; }
        public decimal ServerTipOutForSAs { get; set; }
        public ServerGroupDto ServerGroup { get; set; }
    }
}
