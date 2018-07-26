using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TipTapApi.Migrations
{
    public partial class AddedBarbackTipoutsToTipoutClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BarBackCashTipOut",
                table: "TipOuts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BarBackTipOut",
                table: "TipOuts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarBackCashTipOut",
                table: "TipOuts");

            migrationBuilder.DropColumn(
                name: "BarBackTipOut",
                table: "TipOuts");
        }
    }
}
