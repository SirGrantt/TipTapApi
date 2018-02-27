using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TipTapApi.Migrations
{
    public partial class TipTapDBRolesMigrationFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TipOutPercent",
                table: "Roles",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TipOutPercent",
                table: "ApprovedRoles",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipOutPercent",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "TipOutPercent",
                table: "ApprovedRoles");
        }
    }
}
