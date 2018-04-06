using Common.DTOs.JobDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.StaffMemberDtos
{
    public class StaffMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
    }
}
