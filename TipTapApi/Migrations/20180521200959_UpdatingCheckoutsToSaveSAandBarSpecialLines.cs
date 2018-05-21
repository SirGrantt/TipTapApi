using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class UpdatingCheckoutsToSaveSAandBarSpecialLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BarSpecialLine",
                table: "CheckOuts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaSpecialLine",
                table: "CheckOuts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarSpecialLine",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "SaSpecialLine",
                table: "CheckOuts");
        }
    }
}
