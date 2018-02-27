using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TipTapApi.Entities
{
    public class StaffMemberContext : DbContext
    {
        public StaffMemberContext(DbContextOptions<StaffMemberContext> options)
            :base(options)
        {
            Database.Migrate();
        }

        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<ApprovedRole> ApprovedRoles { get; set; }

    }
}
