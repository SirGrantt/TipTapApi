using Common.DTOs.JobDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.StaffMemberDtos
{
    public class AddApprovedRolesToStaffMember
    {
        public int StaffMemberId { get; set; }
        public List<JobDto> JobsToAdd { get; set; }
    }
}
