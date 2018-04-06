using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class AddingTipOutRelationshipToServerTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TipOuts_ServerTeamId",
                table: "TipOuts");

            migrationBuilder.AddColumn<int>(
                name: "TipOutId",
                table: "ServerTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TipOuts_ServerTeamId",
                table: "TipOuts",
                column: "ServerTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTeams_TipOutId",
                table: "ServerTeams",
                column: "TipOutId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerTeams_TipOuts_TipOutId",
                table: "ServerTeams",
                column: "TipOutId",
                principalTable: "TipOuts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerTeams_TipOuts_TipOutId",
                table: "ServerTeams");

            migrationBuilder.DropIndex(
                name: "IX_TipOuts_ServerTeamId",
                table: "TipOuts");

            migrationBuilder.DropIndex(
                name: "IX_ServerTeams_TipOutId",
                table: "ServerTeams");

            migrationBuilder.DropColumn(
                name: "TipOutId",
                table: "ServerTeams");

            migrationBuilder.CreateIndex(
                name: "IX_TipOuts_ServerTeamId",
                table: "TipOuts",
                column: "ServerTeamId",
                unique: true);
        }
    }
}
