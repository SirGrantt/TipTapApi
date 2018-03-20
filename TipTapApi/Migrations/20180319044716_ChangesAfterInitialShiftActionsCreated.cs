using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class ChangesAfterInitialShiftActionsCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShiftDate",
                table: "Shifts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunchOrDinner",
                table: "Shifts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShiftDateFormatted",
                table: "Shifts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LunchOrDinner",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftDateFormatted",
                table: "Shifts");

            migrationBuilder.AlterColumn<string>(
                name: "ShiftDate",
                table: "Shifts",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
