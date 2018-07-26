using Domain.Jobs;
using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Checkouts
{
    public class SupportHours
    {
        public int Id { get; set; }
        public decimal HoursWorked { get; set; }
        public Job JobWorked { get; set; }
        public StaffMember StaffMember { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }
    }
}
