using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class ChangingTipoutFromBeingTiedToServerTeamsToFittingAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "ServerTeamId",
                table: "TipOuts",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TipOuts_ServerTeamId",
                table: "TipOuts",
                newName: "IX_TipOuts_TeamId");

            migrationBuilder.AddColumn<string>(
                name: "LunchOrDinner",
                table: "TipOuts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamType",
                table: "TipOuts",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TipOuts_Teams_TeamId",
                table: "TipOuts",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipOuts_Teams_TeamId",
                table: "TipOuts");

            migrationBuilder.DropColumn(
                name: "LunchOrDinner",
                table: "TipOuts");

            migrationBuilder.DropColumn(
                name: "TeamType",
                table: "TipOuts");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TipOuts",
                newName: "ServerTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TipOuts_TeamId",
                table: "TipOuts",
                newName: "IX_TipOuts_ServerTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipOuts_Teams_ServerTeamId",
                table: "TipOuts",
                column: "ServerTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
