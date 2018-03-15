using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TipTapApi.Entities
{
    public class StaffMemberContextBAD : DbContext
    {
        public StaffMemberContextBAD(DbContextOptions<StaffMemberContextBAD> options)
            :base(options)
        {
            Database.Migrate();
        }

        public DbSet<StaffMemberOLD> StaffMembers { get; set; }
        public DbSet<JobCode> JobCodes { get; set; }

    }
}
