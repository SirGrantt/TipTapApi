using Common.Entities;
using Domain.StaffMembers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Contexts
{
    public class CheckoutManagerContext : DbContext
    {
        public CheckoutManagerContext(DbContextOptions<CheckoutManagerContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<StaffMemberEntity> StaffMembers { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<CheckoutEntity> CheckOuts { get; set; }
        public DbSet<EarningsEntity> Earnings { get; set; }
        public DbSet<JobEntity> Jobs { get; set; }
        public DbSet<TipOutEntity> TipOuts { get; set; }
        public DbSet<ApprovedJobEntity> ApprovedRoles { get; set; }
        public DbSet<MainJobEntity> MainJobs { get; set; }
        public DbSet<TeamCheckoutEntity> TeamCheckouts { get; set; }
        public DbSet<SupportHoursEntity> SupportHours { get; set; }


    }
}
