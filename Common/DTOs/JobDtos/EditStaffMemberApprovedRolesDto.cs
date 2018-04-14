using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.JobDtos
{
    public class EditStaffMemberApprovedRolesDto
    {
        public int StaffMemberId { get; set; }
        public List<int> JobIds { get; set; }
    }
}
