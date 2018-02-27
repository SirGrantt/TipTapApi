using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TipTapApi.Models
{
    public class StaffMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<RoleDto> ApprovedRoles { get; set; }
        = new List<RoleDto>();
    }
}
