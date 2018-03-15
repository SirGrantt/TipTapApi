using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Jobs
{
    public class BarBack
    {
        public int Id { get; set; }
        public StaffMember Employee { get; set; }
        public decimal Hours { get; set; }
        public decimal TipoutReceived { get; set; }
    }
}
