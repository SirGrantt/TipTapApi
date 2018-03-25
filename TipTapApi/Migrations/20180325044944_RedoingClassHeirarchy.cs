using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class RedoingClassHeirarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerTeams_ServerGroups_ServerGroupId",
                table: "ServerTeams");

            migrationBuilder.DropTable(
                name: "ServerGroups");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_ServerTeams_ServerGroupId",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "BarTipOut",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "IndividualFinalAutoGratTakeHome",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "IndividualFinalCCandAutoGratTakeHome",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "IndividualFinalCashTakeHome",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "IndividualFinalCcTips",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "NumberOfBottlesSold",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "SATipOut",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "ServerGroupId",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "TeamCcTipsAfterCheckout",
                table: "ServerTeams");

            migrationBuilder.AddColumn<DateTime>(
                name: "ShiftDate",
                table: "ServerTeams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                    ServerTeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckOuts_ServerTeams_ServerTeamId",
                        column: x => x.ServerTeamId,
                        principalTable: "ServerTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CheckOuts_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    StaffMemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Jobs_StaffMemberId",
                table: "Jobs",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TipOuts_ServerTeamId",
                table: "TipOuts",
                column: "ServerTeamId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckOuts");

            migrationBuilder.DropTable(
                name: "Earnings");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "TipOuts");

            migrationBuilder.DropColumn(
                name: "ShiftDate",
                table: "ServerTeams");

            migrationBuilder.AddColumn<decimal>(
                name: "BarTipOut",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndividualFinalAutoGratTakeHome",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndividualFinalCCandAutoGratTakeHome",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndividualFinalCashTakeHome",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IndividualFinalCcTips",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBottlesSold",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SATipOut",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ServerGroupId",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TeamCcTipsAfterCheckout",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutoGratTakeHome = table.Column<decimal>(nullable: false),
                    BarSales = table.Column<decimal>(nullable: false),
                    CashAutoGratuity = table.Column<decimal>(nullable: false),
                    CashTipTakeHome = table.Column<decimal>(nullable: false),
                    CashTips = table.Column<decimal>(nullable: false),
                    CcAutoGratuity = table.Column<decimal>(nullable: false),
                    CcTipTakehome = table.Column<decimal>(nullable: false),
                    CcTips = table.Column<decimal>(nullable: false),
                    GrossSales = table.Column<decimal>(nullable: false),
                    Hours = table.Column<decimal>(nullable: false),
                    NonTipoutBarSales = table.Column<decimal>(nullable: false),
                    Sales = table.Column<decimal>(nullable: false),
                    ServerTeamId = table.Column<int>(nullable: false),
                    StaffMemberId = table.Column<int>(nullable: false),
                    TotalTipTakehome = table.Column<decimal>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LunchOrDinner = table.Column<string>(nullable: true),
                    ServerTipOutForBartenders = table.Column<decimal>(nullable: false),
                    ServerTipOutForSAs = table.Column<decimal>(nullable: false),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    ShiftDateFormatted = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_ServerTeams_ServerGroupId",
                table: "ServerTeams",
                column: "ServerGroupId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ServerTeams_ServerGroups_ServerGroupId",
                table: "ServerTeams",
                column: "ServerGroupId",
                principalTable: "ServerGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
