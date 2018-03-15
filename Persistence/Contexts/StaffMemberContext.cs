using Common.Entities;
using Domain.StaffMembers;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Contexts
{
    public class StaffMemberContext : DbContext
    {
        public StaffMemberContext(DbContextOptions<StaffMemberContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<StaffMemberEntity> StaffMembers { get; set; }

    }
}
