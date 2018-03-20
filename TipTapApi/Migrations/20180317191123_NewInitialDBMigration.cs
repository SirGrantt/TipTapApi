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
                name: "Shifts",
                columns: table => new
                {
                    ShiftDate = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServerTipOutForBartenders = table.Column<decimal>(nullable: false),
                    ServerTipOutForSAs = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServersTipOutToBar = table.Column<decimal>(nullable: false),
                    ServersTipOutToSAs = table.Column<decimal>(nullable: false),
                    ShiftId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerGroups_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckoutHasBeenRun = table.Column<bool>(nullable: false),
                    IndividualFinalCcTips = table.Column<decimal>(nullable: false),
                    TeamCcTipsAfterCheckout = table.Column<decimal>(nullable: false),
                    IndividualFinalAutoGratTakeHome = table.Column<decimal>(nullable: false),
                    IndividualFinalCCandAutoGratTakeHome = table.Column<decimal>(nullable: false),
                    IndividualFinalCashTakeHome = table.Column<decimal>(nullable: false),
                    BarTipOut = table.Column<decimal>(nullable: false),
                    SATipOut = table.Column<decimal>(nullable: false),
                    NumberOfBottlesSold = table.Column<int>(nullable: false),
                    ServerGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerTeams_ServerGroups_ServerGroupId",
                        column: x => x.ServerGroupId,
                        principalTable: "ServerGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrossSales = table.Column<decimal>(nullable: false),
                    Sales = table.Column<decimal>(nullable: false),
                    BarSales = table.Column<decimal>(nullable: false),
                    CcTips = table.Column<decimal>(nullable: false),
                    CashTips = table.Column<decimal>(nullable: false),
                    CcAutoGratuity = table.Column<decimal>(nullable: false),
                    CashAutoGratuity = table.Column<decimal>(nullable: false),
                    NonTipoutBarSales = table.Column<decimal>(nullable: false),
                    Hours = table.Column<decimal>(nullable: false),
                    CcTipTakehome = table.Column<decimal>(nullable: false),
                    CashTipTakeHome = table.Column<decimal>(nullable: false),
                    AutoGratTakeHome = table.Column<decimal>(nullable: false),
                    TotalTipTakehome = table.Column<decimal>(nullable: false),
                    ServerTeamId = table.Column<int>(nullable: false),
                    StaffMemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_ServerTeams_ServerTeamId",
                        column: x => x.ServerTeamId,
                        principalTable: "ServerTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servers_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerGroups_ShiftId",
                table: "ServerGroups",
                column: "ShiftId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servers_ServerTeamId",
                table: "Servers",
                column: "ServerTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_StaffMemberId",
                table: "Servers",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTeams_ServerGroupId",
                table: "ServerTeams",
                column: "ServerGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "ServerTeams");

            migrationBuilder.DropTable(
                name: "StaffMembers");

            migrationBuilder.DropTable(
                name: "ServerGroups");

            migrationBuilder.DropTable(
                name: "Shifts");
        }
    }
}
