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
    [DbContext(typeof(CheckoutManagerContext))]
    [Migration("20180403050758_RemovingErroneousServerTeamTipOutRelationship")]
    partial class RemovingErroneousServerTeamTipOutRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview1-28290")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Common.Entities.ApprovedJobEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobId");

                    b.Property<int>("StaffMemberId");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("ApprovedRoles");
                });

            modelBuilder.Entity("Common.Entities.CheckoutEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BarSales");

                    b.Property<decimal>("CashAutoGrat");

                    b.Property<decimal>("CashTips");

                    b.Property<decimal>("CcAutoGrat");

                    b.Property<decimal>("CcTips");

                    b.Property<decimal>("GrossSales");

                    b.Property<decimal>("Hours");

                    b.Property<int>("JobWorkedId");

                    b.Property<string>("LunchOrDinner");

                    b.Property<decimal>("NonTipOutBarSales");

                    b.Property<int>("NumberOfBottlesSold");

                    b.Property<decimal>("Sales");

                    b.Property<DateTime>("ShiftDate");

                    b.Property<int>("StaffMemberId");

                    b.HasKey("Id");

                    b.HasIndex("JobWorkedId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("CheckOuts");
                });

            modelBuilder.Entity("Common.Entities.EarningsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AutoGratuity");

                    b.Property<decimal>("CashPayedForTipOut");

                    b.Property<decimal>("CashTips");

                    b.Property<decimal>("CcTips");

                    b.Property<string>("JobWorked")
                        .IsRequired();

                    b.Property<bool>("OwedCashForTipOut");

                    b.Property<DateTime>("ShiftDate");

                    b.Property<int>("StaffMemberId");

                    b.Property<decimal>("TotalTipsForPayroll");

                    b.HasKey("Id");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("Earnings");
                });

            modelBuilder.Entity("Common.Entities.JobEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Common.Entities.MainJobEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobId");

                    b.Property<int>("StaffMemberId");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("MainJobs");
                });

            modelBuilder.Entity("Common.Entities.ServerTeamCheckoutEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CheckoutId");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("CheckoutId");

                    b.HasIndex("TeamId");

                    b.ToTable("ServerTeamCheckouts");
                });

            modelBuilder.Entity("Common.Entities.ServerTeamEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CheckoutHasBeenRun");

                    b.Property<string>("LunchOrDinner");

                    b.Property<DateTime>("ShiftDate");

                    b.HasKey("Id");

                    b.ToTable("ServerTeams");
                });

            modelBuilder.Entity("Common.Entities.StaffMemberEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("StaffMembers");
                });

            modelBuilder.Entity("Common.Entities.TipOutEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BarTipOut");

                    b.Property<decimal>("FinalTeamBarSales");

                    b.Property<decimal>("SaTipOut");

                    b.Property<int>("ServerTeamId");

                    b.Property<DateTime>("ShiftDate");

                    b.Property<decimal>("TeamGrossSales");

                    b.HasKey("Id");

                    b.HasIndex("ServerTeamId");

                    b.ToTable("TipOuts");
                });

            modelBuilder.Entity("Common.Entities.ApprovedJobEntity", b =>
                {
                    b.HasOne("Common.Entities.JobEntity", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Entities.StaffMemberEntity", "StaffMember")
                        .WithMany()
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entities.CheckoutEntity", b =>
                {
                    b.HasOne("Common.Entities.JobEntity", "Job")
                        .WithMany()
                        .HasForeignKey("JobWorkedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Entities.StaffMemberEntity", "StaffMember")
                        .WithMany("CheckOuts")
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entities.EarningsEntity", b =>
                {
                    b.HasOne("Common.Entities.StaffMemberEntity", "StaffMember")
                        .WithMany("Earnings")
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entities.MainJobEntity", b =>
                {
                    b.HasOne("Common.Entities.JobEntity", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Entities.StaffMemberEntity", "StaffMember")
                        .WithMany()
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entities.ServerTeamCheckoutEntity", b =>
                {
                    b.HasOne("Common.Entities.CheckoutEntity", "Checkout")
                        .WithMany()
                        .HasForeignKey("CheckoutId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Entities.ServerTeamEntity", "ServerTeam")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entities.TipOutEntity", b =>
                {
                    b.HasOne("Common.Entities.ServerTeamEntity", "ServerTeam")
                        .WithMany()
                        .HasForeignKey("ServerTeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
