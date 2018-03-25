﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Persistence.Contexts;

namespace TipTapApi.Migrations
{
    [DbContext(typeof(CheckOutManagerContext))]
    [Migration("20180317191123_NewInitialDBMigration")]
    partial class NewInitialDBMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview1-28290")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Common.Entities.ServerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AutoGratTakeHome");

                    b.Property<decimal>("BarSales");

                    b.Property<decimal>("CashAutoGratuity");

                    b.Property<decimal>("CashTipTakeHome");

                    b.Property<decimal>("CashTips");

                    b.Property<decimal>("CcAutoGratuity");

                    b.Property<decimal>("CcTipTakehome");

                    b.Property<decimal>("CcTips");

                    b.Property<decimal>("GrossSales");

                    b.Property<decimal>("Hours");

                    b.Property<decimal>("NonTipoutBarSales");

                    b.Property<decimal>("Sales");

                    b.Property<int>("ServerTeamId");

                    b.Property<int>("StaffMemberId");

                    b.Property<decimal>("TotalTipTakehome");

                    b.HasKey("Id");

                    b.HasIndex("ServerTeamId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Common.Entities.ServerGroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ServersTipOutToBar");

                    b.Property<decimal>("ServersTipOutToSAs");

                    b.Property<int>("ShiftId");

                    b.HasKey("Id");

                    b.HasIndex("ShiftId")
                        .IsUnique();

                    b.ToTable("ServerGroups");
                });

            modelBuilder.Entity("Common.Entities.ServerTeamEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BarTipOut");

                    b.Property<bool>("CheckoutHasBeenRun");

                    b.Property<decimal>("IndividualFinalAutoGratTakeHome");

                    b.Property<decimal>("IndividualFinalCCandAutoGratTakeHome");

                    b.Property<decimal>("IndividualFinalCashTakeHome");

                    b.Property<decimal>("IndividualFinalCcTips");

                    b.Property<int>("NumberOfBottlesSold");

                    b.Property<decimal>("SATipOut");

                    b.Property<int>("ServerGroupId");

                    b.Property<decimal>("TeamCcTipsAfterCheckout");

                    b.HasKey("Id");

                    b.HasIndex("ServerGroupId");

                    b.ToTable("ServerTeams");
                });

            modelBuilder.Entity("Common.Entities.ShiftEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ServerTipOutForBartenders");

                    b.Property<decimal>("ServerTipOutForSAs");

                    b.Property<string>("ShiftDate");

                    b.HasKey("Id");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("Common.Entities.StaffMemberEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("StaffMembers");
                });

            modelBuilder.Entity("Common.Entities.ServerEntity", b =>
                {
                    b.HasOne("Common.Entities.ServerTeamEntity", "ServerTeam")
                        .WithMany("Servers")
                        .HasForeignKey("ServerTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Entities.StaffMemberEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entities.ServerGroupEntity", b =>
                {
                    b.HasOne("Common.Entities.ShiftEntity", "Shift")
                        .WithOne("ServerGroup")
                        .HasForeignKey("Common.Entities.ServerGroupEntity", "ShiftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entities.ServerTeamEntity", b =>
                {
                    b.HasOne("Common.Entities.ServerGroupEntity", "ServerGroup")
                        .WithMany("ServerTeams")
                        .HasForeignKey("ServerGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
