using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class ServerCheckoutCrossReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerTeamCheckouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<int>(nullable: false),
                    CheckoutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTeamCheckouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerTeamCheckouts_CheckOuts_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "CheckOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerTeamCheckouts_ServerTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "ServerTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerTeamCheckouts_CheckoutId",
                table: "ServerTeamCheckouts",
                column: "CheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTeamCheckouts_TeamId",
                table: "ServerTeamCheckouts",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerTeamCheckouts");
        }
    }
}
