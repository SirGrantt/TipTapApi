using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class AddedSupportHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "SupportHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HoursWorked = table.Column<decimal>(nullable: false),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    LunchOrDinner = table.Column<string>(nullable: true),
                    StaffMemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportHours", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamCheckouts_CheckOuts_CheckoutId",
                table: "TeamCheckouts");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamCheckouts_Teams_TeamId",
                table: "TeamCheckouts");

            migrationBuilder.DropForeignKey(
                name: "FK_TipOuts_Teams_ServerTeamId",
                table: "TipOuts");

            migrationBuilder.DropTable(
                name: "SupportHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamCheckouts",
                table: "TeamCheckouts");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "ServerTeams");

            migrationBuilder.RenameTable(
                name: "TeamCheckouts",
                newName: "ServerTeamCheckouts");

            migrationBuilder.RenameColumn(
                name: "TeamType",
                table: "ServerTeams",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_TeamCheckouts_TeamId",
                table: "ServerTeamCheckouts",
                newName: "IX_ServerTeamCheckouts_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamCheckouts_CheckoutId",
                table: "ServerTeamCheckouts",
                newName: "IX_ServerTeamCheckouts_CheckoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServerTeams",
                table: "ServerTeams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServerTeamCheckouts",
                table: "ServerTeamCheckouts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerTeamCheckouts_CheckOuts_CheckoutId",
                table: "ServerTeamCheckouts",
                column: "CheckoutId",
                principalTable: "CheckOuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerTeamCheckouts_ServerTeams_TeamId",
                table: "ServerTeamCheckouts",
                column: "TeamId",
                principalTable: "ServerTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipOuts_ServerTeams_ServerTeamId",
                table: "TipOuts",
                column: "ServerTeamId",
                principalTable: "ServerTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
