using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TipTapApi.Migrations
{
    public partial class TipTapDBRolesBetterDefined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedRoles_StaffMembers_StaffMemberId",
                table: "ApprovedRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApprovedRoles",
                table: "ApprovedRoles");

            migrationBuilder.DropIndex(
                name: "IX_ApprovedRoles_StaffMemberId",
                table: "ApprovedRoles");

            migrationBuilder.DropColumn(
                name: "StaffMemberId",
                table: "ApprovedRoles");

            migrationBuilder.RenameTable(
                name: "ApprovedRoles",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ApprovedRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffMemberId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRoles_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRoles_StaffMemberId",
                table: "ApprovedRoles",
                column: "StaffMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovedRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "ApprovedRoles");

            migrationBuilder.AddColumn<int>(
                name: "StaffMemberId",
                table: "ApprovedRoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApprovedRoles",
                table: "ApprovedRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRoles_StaffMemberId",
                table: "ApprovedRoles",
                column: "StaffMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedRoles_StaffMembers_StaffMemberId",
                table: "ApprovedRoles",
                column: "StaffMemberId",
                principalTable: "StaffMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
