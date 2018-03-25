using Common.DTOs.JobDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.StaffMemberDtos
{
    public class AddApprovedRolesToStaffMember
    {
        public List<JobDto> JobsToAdd { get; set; }
    }
}
