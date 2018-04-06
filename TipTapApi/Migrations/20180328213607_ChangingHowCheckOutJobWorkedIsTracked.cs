using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class ChangingHowCheckOutJobWorkedIsTracked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobWorked",
                table: "CheckOuts");

            migrationBuilder.AddColumn<int>(
                name: "JobWorkedId",
                table: "CheckOuts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_JobWorkedId",
                table: "CheckOuts",
                column: "JobWorkedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Jobs_JobWorkedId",
                table: "CheckOuts",
                column: "JobWorkedId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Jobs_JobWorkedId",
                table: "CheckOuts");

            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_JobWorkedId",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "JobWorkedId",
                table: "CheckOuts");

            migrationBuilder.AddColumn<string>(
                name: "JobWorked",
                table: "CheckOuts",
                nullable: true);
        }
    }
}
