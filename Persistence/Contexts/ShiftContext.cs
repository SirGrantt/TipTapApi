using Common.Entities;
using Domain.StaffMembers;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Contexts
{
    public class ShiftContext : DbContext
    {
        public ShiftContext(DbContextOptions<ShiftContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ShiftEntity> Shifts { get; set; }
        public DbSet<StaffMemberEntity> StaffMembers { get; set; }
        public DbSet<ServerEntity> Servers { get; set; }
        public DbSet<ServerGroupEntity> ServerGroups { get; set; }
        public DbSet<ServerTeamEntity> ServerTeams { get; set; }

    }
}
