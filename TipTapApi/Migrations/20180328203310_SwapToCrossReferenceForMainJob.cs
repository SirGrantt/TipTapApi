using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class SwapToCrossReferenceForMainJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_Jobs_JobId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_JobId",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "StaffMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "StaffMembers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_JobId",
                table: "StaffMembers",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_Jobs_JobId",
                table: "StaffMembers",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
