﻿using Common.DTOs.JobDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.StaffMemberDtos
{
    public class RemoveRolesFromStaffMember
    {
        public int StaffMemberId { get; set; }
        public List<JobDto> JobsToRemove { get; set; }
    }
}
