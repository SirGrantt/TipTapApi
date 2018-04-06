using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class RemovingErroneousServerTeamTipOutRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerTeams_TipOuts_TipOutId",
                table: "ServerTeams");

            migrationBuilder.DropIndex(
                name: "IX_ServerTeams_TipOutId",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "TipOutId",
                table: "ServerTeams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipOutId",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServerTeams_TipOutId",
                table: "ServerTeams",
                column: "TipOutId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerTeams_TipOuts_TipOutId",
                table: "ServerTeams",
                column: "TipOutId",
                principalTable: "TipOuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
