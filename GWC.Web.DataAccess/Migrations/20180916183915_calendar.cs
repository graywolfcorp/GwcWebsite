using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GWC.Web.DataAccess.Migrations
{
    public partial class calendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Calendars");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Calendars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Calendars");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Calendars",
                maxLength: 50,
                nullable: true);
        }
    }
}
