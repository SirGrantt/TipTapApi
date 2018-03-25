using Common.Entities;
using Domain.StaffMembers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Contexts
{
    public class CheckOutManagerContext : DbContext
    {
        public CheckOutManagerContext(DbContextOptions<CheckOutManagerContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<StaffMemberEntity> StaffMembers { get; set; }
        public DbSet<ServerTeamEntity> ServerTeams { get; set; }
        public DbSet<CheckOutEntity> CheckOuts { get; set; }
        public DbSet<EarningsEntity> Earnings { get; set; }
        public DbSet<JobEntity> Jobs { get; set; }
        public DbSet<TipOutEntity> TipOuts { get; set; }

    }
}
