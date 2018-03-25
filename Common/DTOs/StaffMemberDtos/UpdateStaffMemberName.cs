using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.StaffMemberDtos
{
    public class UpdateStaffMemberName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReasonForChange { get; set; }
    }
}
