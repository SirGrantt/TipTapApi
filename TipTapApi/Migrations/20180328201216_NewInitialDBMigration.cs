using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class NewInitialDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    CheckoutHasBeenRun = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    JobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffMembers_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TipOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamGrossSales = table.Column<decimal>(nullable: false),
                    FinalTeamBarSales = table.Column<decimal>(nullable: false),
                    SaTipOut = table.Column<decimal>(nullable: false),
                    BarTipOut = table.Column<decimal>(nullable: false),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    ServerTeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipOuts_ServerTeams_ServerTeamId",
                        column: x => x.ServerTeamId,
                        principalTable: "ServerTeams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovedRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffMemberId = table.Column<int>(nullable: false),
                    JobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRoles_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRoles_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CheckOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    NonTipOutBarSales = table.Column<decimal>(nullable: false),
                    NumberOfBottlesSold = table.Column<int>(nullable: false),
                    JobWorked = table.Column<string>(nullable: true),
                    LunchOrDinner = table.Column<string>(nullable: true),
                    Sales = table.Column<decimal>(nullable: false),
                    GrossSales = table.Column<decimal>(nullable: false),
                    BarSales = table.Column<decimal>(nullable: false),
                    CcTips = table.Column<decimal>(nullable: false),
                    CashTips = table.Column<decimal>(nullable: false),
                    CcAutoGrat = table.Column<decimal>(nullable: false),
                    CashAutoGrat = table.Column<decimal>(nullable: false),
                    Hours = table.Column<decimal>(nullable: false),
                    StaffMemberId = table.Column<int>(nullable: false),
                    ServerTeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckOuts_ServerTeams_ServerTeamId",
                        column: x => x.ServerTeamId,
                        principalTable: "ServerTeams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CheckOuts_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Earnings",
                columns: table => new
                {
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobWorked = table.Column<string>(nullable: false),
                    OwedCashForTipOut = table.Column<bool>(nullable: false),
                    CashPayedForTipOut = table.Column<decimal>(nullable: false),
                    CcTips = table.Column<decimal>(nullable: false),
                    AutoGratuity = table.Column<decimal>(nullable: false),
                    TotalTipsForPayroll = table.Column<decimal>(nullable: false),
                    CashTips = table.Column<decimal>(nullable: false),
                    StaffMemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Earnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Earnings_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRoles_JobId",
                table: "ApprovedRoles",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRoles_StaffMemberId",
                table: "ApprovedRoles",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_ServerTeamId",
                table: "CheckOuts",
                column: "ServerTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_StaffMemberId",
                table: "CheckOuts",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Earnings_StaffMemberId",
                table: "Earnings",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_JobId",
                table: "StaffMembers",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_TipOuts_ServerTeamId",
                table: "TipOuts",
                column: "ServerTeamId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovedRoles");

            migrationBuilder.DropTable(
                name: "CheckOuts");

            migrationBuilder.DropTable(
                name: "Earnings");

            migrationBuilder.DropTable(
                name: "TipOuts");

            migrationBuilder.DropTable(
                name: "StaffMembers");

            migrationBuilder.DropTable(
                name: "ServerTeams");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
