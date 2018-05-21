using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckOutDtos
{
    public class GetCheckoutByDateDto
    {
        public int StaffMemberId { get; set; }
        public string LunchOrDinner { get; set; }
        public string StringDate { get; set; }
    }
}
