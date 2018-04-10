using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class AddingTableForIndividualServerTipouts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndividualServerTipouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrossSales = table.Column<decimal>(nullable: false),
                    FinalBarSales = table.Column<decimal>(nullable: false),
                    SaTipOut = table.Column<decimal>(nullable: false),
                    BarTipOut = table.Column<decimal>(nullable: false),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    StaffMemberId = table.Column<int>(nullable: false),
                    LunchOrDinner = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualServerTipouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualServerTipouts_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualServerTipouts_StaffMemberId",
                table: "IndividualServerTipouts",
                column: "StaffMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualServerTipouts");
        }
    }
}
